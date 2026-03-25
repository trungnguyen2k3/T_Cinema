using CinemaBE.Dtos;
using CinemaBE.Hubs;
using CinemaBE.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CinemaBE.Services
{
    public class ChatServiceImpl : IChatService
    {
        private readonly DatabaseContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatServiceImpl(DatabaseContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<ChatMessageDto> SendMessageAsync(SendMessageRequest request)
        {
            if (request.SenderId <= 0 || request.ReceiverId <= 0)
                throw new Exception("SenderId hoặc ReceiverId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(request.Content))
                throw new Exception("Nội dung tin nhắn không được để trống.");

            var senderExists = await _context.SysAccounts.AnyAsync(x => x.Id == request.SenderId);
            var receiverExists = await _context.SysAccounts.AnyAsync(x => x.Id == request.ReceiverId);

            if (!senderExists || !receiverExists)
                throw new Exception("Sender hoặc Receiver không tồn tại.");

            var entity = new TblChat
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Content = request.Content.Trim(),
                MessageType = string.IsNullOrWhiteSpace(request.MessageType)
                    ? "TEXT"
                    : request.MessageType.Trim().ToUpper(),
                IsRead = false,
                CreatedAt = DateTime.Now,
                ReadAt = null
            };

            _context.TblChats.Add(entity);
            await _context.SaveChangesAsync();

            var result = new ChatMessageDto
            {
                Id = entity.Id,
                SenderId = entity.SenderId,
                ReceiverId = entity.ReceiverId,
                Content = entity.Content,
                MessageType = entity.MessageType,
                IsRead = entity.IsRead,
                CreatedAt = entity.CreatedAt,
                ReadAt = entity.ReadAt
            };

            Console.WriteLine($"[ChatService] Send ReceiveMessage -> group user_{entity.ReceiverId}");
            await _hubContext.Clients.Group($"user_{entity.ReceiverId}")
                .SendAsync("ReceiveMessage", result);

            Console.WriteLine($"[ChatService] Send ReceiveOwnMessage -> group user_{entity.SenderId}");
            await _hubContext.Clients.Group($"user_{entity.SenderId}")
                .SendAsync("ReceiveOwnMessage", result);

            return result;
        }

        public async Task<List<ChatMessageDto>> GetConversationAsync(int user1Id, int user2Id)
        {
            return await _context.TblChats
                .Where(x =>
                    (x.SenderId == user1Id && x.ReceiverId == user2Id) ||
                    (x.SenderId == user2Id && x.ReceiverId == user1Id))
                .OrderBy(x => x.CreatedAt)
                .Select(x => new ChatMessageDto
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    ReceiverId = x.ReceiverId,
                    Content = x.Content,
                    MessageType = x.MessageType,
                    IsRead = x.IsRead,
                    CreatedAt = x.CreatedAt,
                    ReadAt = x.ReadAt
                })
                .ToListAsync();
        }

        public async Task<int> MarkAsReadAsync(int currentUserId, int chatUserId)
        {
            var messages = await _context.TblChats
                .Where(x => x.SenderId == chatUserId
                         && x.ReceiverId == currentUserId
                         && !x.IsRead)
                .ToListAsync();

            if (!messages.Any())
                return 0;

            foreach (var item in messages)
            {
                item.IsRead = true;
                item.ReadAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            Console.WriteLine($"[ChatService] Send MessagesRead -> group user_{chatUserId}");
            await _hubContext.Clients.Group($"user_{chatUserId}")
                .SendAsync("MessagesRead", new
                {
                    readerId = currentUserId,
                    senderId = chatUserId,
                    count = messages.Count
                });

            return messages.Count;
        }
    }
}