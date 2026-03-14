namespace PTruequeU.DTOs.Chat
{
    public class ChatRoomResponseDto
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string ListingTitle { get; set; } = string.Empty;
        public string BuyerId { get; set; } = string.Empty;
        public string BuyerName { get; set; } = string.Empty;
        public string SellerId { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ChatMessageDto? LastMessage { get; set; }
    }
}
