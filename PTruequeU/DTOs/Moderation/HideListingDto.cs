using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Moderation
{
    public class HideListingDto
    {
        [Required]
        public int ListingId { get; set; }

        [MaxLength(1000)]
        public string? Reason { get; set; }
    }
}
