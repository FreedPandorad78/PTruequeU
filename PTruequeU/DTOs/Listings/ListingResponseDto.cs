using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class ListingResponseDto
    {
        public Guid ListingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ListingCondition Condition { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public ListingState State { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }

        public List<ListingImageDto> Images { get; set; } = new();
    }
}