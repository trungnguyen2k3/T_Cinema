using CinemaBE.Dtos;
using CinemaBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBE.Areas.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        // Hàm POSt gửi tin nhắn từ client lên server
        // endpoint:  api/chat/send
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            try
            {
                var result = await _chatService.SendMessageAsync(request);
                return Ok(new
                {
                    success = true,
                    message = "Gửi tin nhắn thành công",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // Hàm POST lấy cuộc trò chuyện giữa 2 người dùng
        // GET: api/chat/conversation?user1Id=1&user2Id=2
        [HttpGet("conversation")]
        public async Task<IActionResult> GetConversation([FromQuery] int user1Id, [FromQuery] int user2Id)
        {
            try
            {
                var result = await _chatService.GetConversationAsync(user1Id, user2Id);
                return Ok(new
                {
                    success = true,
                    message = "Lấy cuộc trò chuyện thành công",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        // Hàm PUT đánh dấu tất cả tin nhắn từ chatUserId đến currentUserId là đã đọc

        [HttpPut("mark-as-read")]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkAsReadRequest request)
        {
            try
            {
                var count = await _chatService.MarkAsReadAsync(request.CurrentUserId, request.ChatUserId);
                return Ok(new
                {
                    success = true,
                    message = "Đánh dấu đã đọc thành công",
                    data = count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}