using PTruequeU.DTOs.Chat;

namespace PTruequeU.Interfaces
{
    public interface IChatService
    {
        Task<ChatRoomResponseDto?> StartOrGetChatAsync(string buyerId, int listingId);
        Task<ChatMessageDto?> SendMessageAsync(int chatRoomId, string senderId, CreateChatMessageDto dto);
        Task<List<ChatMessageDto>> GetMessagesAsync(int chatRoomId, string userId);
        Task<List<ChatRoomResponseDto>> GetUserChatsAsync(string userId);
    }
}
