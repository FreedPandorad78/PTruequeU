using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class ListingSearchDto
    {
        public string? Keyword { get; set; }
        public Guid? CategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public ListingCondition? Condition { get; set; }
        public ListingState? State { get; set; }
        public DateTime? PostedAfter { get; set; }
        public DateTime? PostedBefore { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}