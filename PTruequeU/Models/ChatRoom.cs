namespace PTruequeU.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;

        public string BuyerId { get; set; } = string.Empty;
        public ApplicationUser Buyer { get; set; } = null!;

        public string SellerId { get; set; } = string.Empty;
        public ApplicationUser Seller { get; set; } = null!;

        // Navigation
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
