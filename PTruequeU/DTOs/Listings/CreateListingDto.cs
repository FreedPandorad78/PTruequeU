using PTruequeU.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Listings
{
    public class CreateListingDto : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public ListingCondition Condition { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public List<string> ImageUrls { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ImageUrls == null || ImageUrls.Count < 3)
            {
                yield return new ValidationResult(
                    "At least 3 images are required.",
                    new[] { nameof(ImageUrls) });
            }
        }
    }
}
