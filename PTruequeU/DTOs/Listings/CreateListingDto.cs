using System.ComponentModel.DataAnnotations;
using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Listings
{
    public class CreateListingDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "El título debe tener entre 5 y 120 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 2000 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La condición es obligatoria.")]
        public ListingCondition Condition { get; set; }

        [Range(0.01, 100000000, ErrorMessage = "El precio debe ser mayor que 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "La ubicación debe tener entre 2 y 120 caracteres.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Debes enviar imágenes.")]
        [MinLength(3, ErrorMessage = "La publicación debe tener al menos 3 imágenes.")]
        public List<string> ImageUrls { get; set; } = new();
    }
}