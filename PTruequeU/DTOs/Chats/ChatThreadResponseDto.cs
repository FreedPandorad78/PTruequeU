namespace PTruequeU.DTOs.Chats
{
    public class ChatThreadResponseDto
    {
        public Guid ChatThreadId { get; set; }
        public Guid ListingId { get; set; }
        public string BuyerId { get; set; } = string.Empty;
        public string SellerId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string ListingTitle { get; set; } = string.Empty;
        public bool ListingIsHidden { get; set; }

        public string? LastMessageText { get; set; }
        public DateTime? LastMessageSentAt { get; set; }
    }
}