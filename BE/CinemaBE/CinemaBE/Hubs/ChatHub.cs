using CinemaBE.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CinemaBE.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DatabaseContext _context;

        public ChatHub(DatabaseContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userIdText = httpContext?.Request.Query["userId"].ToString();

            if (string.IsNullOrWhiteSpace(userIdText))
            {
                Context.Abort();
                return;
            }

            if (!int.TryParse(userIdText, out int userId))
            {
                Context.Abort();
                return;
            }

            var exists = await _context.SysAccounts.AnyAsync(x => x.Id == userId);
            if (!exists)
            {
                Context.Abort();
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");

            Console.WriteLine($"[Connected] ConnectionId={Context.ConnectionId}, UserId={userId}");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext();
            var userIdText = httpContext?.Request.Query["userId"].ToString();

            if (int.TryParse(userIdText, out int userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
                Console.WriteLine($"[Disconnected] ConnectionId={Context.ConnectionId}, UserId={userId}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}