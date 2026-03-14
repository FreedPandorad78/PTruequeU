namespace PTruequeU.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;
    }
}
