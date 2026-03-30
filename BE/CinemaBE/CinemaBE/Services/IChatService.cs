using CinemaBE.Dtos;

namespace CinemaBE.Services
{
    public interface IChatService
    {
        Task<ChatMessageDto> SendMessageAsync(SendMessageRequest request);
        Task<List<ChatMessageDto>> GetConversationAsync(int user1Id, int user2Id);
        Task<int> MarkAsReadAsync(int currentUserId, int chatUserId);
    }
}
