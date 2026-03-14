using PTruequeU.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Listings
{
    public class UpdateListingDto
    {
        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public ListingCondition? Condition { get; set; }

        [Range(0.01, 999999.99)]
        public decimal? Price { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        public int? CategoryId { get; set; }
    }
}
