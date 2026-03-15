using System.ComponentModel.DataAnnotations;
using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class UpdateListingDto
    {
        [StringLength(120, MinimumLength = 5, ErrorMessage = "El título debe tener entre 5 y 120 caracteres.")]
        public string? Title { get; set; }

        [StringLength(2000, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 2000 caracteres.")]
        public string? Description { get; set; }

        public ListingCondition? Condition { get; set; }

        [Range(0.01, 100000000, ErrorMessage = "El precio debe ser mayor que 0.")]
        public double? Price { get; set; }

        [StringLength(120, MinimumLength = 2, ErrorMessage = "La ubicación debe tener entre 2 y 120 caracteres.")]
        public string? Location { get; set; }

        public Guid? CategoryId { get; set; }
    }
}