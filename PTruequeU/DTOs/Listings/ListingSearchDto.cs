using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class ListingSearchDto
    {
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public ListingCondition? Condition { get; set; }
        public ListingState? State { get; set; }
        public DateTime? PostedAfter { get; set; }
        public DateTime? PostedBefore { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
