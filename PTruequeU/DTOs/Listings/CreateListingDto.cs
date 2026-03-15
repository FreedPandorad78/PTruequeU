using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class CreateListingDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ListingCondition Condition { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new();
    }
}