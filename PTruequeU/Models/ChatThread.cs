namespace PTruequeU.Models
{
    public class ChatThread
    {
        public Guid ChatThread_Id { get; set; }
        public Guid Listing_Id { get; set; }
        public Listing? Listing { get; set; }

        public string Buyer_Id { get; set; } = string.Empty;
        public ApplicationUser? Buyer { get; set; }

        public string Seller_Id { get; set; } = string.Empty;
        public ApplicationUser? Seller { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}