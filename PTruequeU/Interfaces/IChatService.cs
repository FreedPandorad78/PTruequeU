using PTruequeU.DTOs.Chats;

namespace PTruequeU.Interfaces
{
    public interface IChatService
    {
        Task<ChatThreadResponseDto?> StartChat(string currentUserId, StartChatRequestDto dto);
        Task<List<ChatThreadResponseDto>> GetMyChats(string currentUserId);
        Task<List<ChatMessageResponseDto>?> GetMessages(Guid threadId, string currentUserId);
        Task<ChatMessageResponseDto?> SendMessage(Guid threadId, string currentUserId, SendChatMessageRequestDto dto);
    }
}