using System.ComponentModel.DataAnnotations;

namespace PTruequeU.Models
{
    public class ListingImage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;
    }
}
