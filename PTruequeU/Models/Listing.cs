using PTruequeU.Models.Enums;

namespace PTruequeU.Models
{
    public class Listing
    {
        public Guid Listing_id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ListingCondition Condition { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;

        public ListingState State { get; set; } = ListingState.Available;
        public bool IsHidden { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Owner
        public string User_Id { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        // Category
        public Guid Category_Id { get; set; }
        public Category? Category { get; set; }

        public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
    }
}