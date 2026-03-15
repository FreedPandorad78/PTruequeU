using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class UpdateListingDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ListingCondition? Condition { get; set; }
        public decimal? Price { get; set; }
        public string? Location { get; set; }
        public Guid? CategoryId { get; set; }
    }
}