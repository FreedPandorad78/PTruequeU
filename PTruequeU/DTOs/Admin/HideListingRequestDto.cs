using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Admin
{
    public class HideListingRequestDto
    {
        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El motivo debe tener entre 3 y 150 caracteres.")]
        public string Reason { get; set; } = string.Empty;
    }
}