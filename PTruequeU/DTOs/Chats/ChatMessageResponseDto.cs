namespace PTruequeU.DTOs.Chats
{
    public class ChatMessageResponseDto
    {
        public Guid ChatMessageId { get; set; }
        public Guid ThreadId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}