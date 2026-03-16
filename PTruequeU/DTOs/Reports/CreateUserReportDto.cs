using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Reports
{
    public class CreateUserReportDto
    {
        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El motivo debe tener entre 3 y 150 caracteres.")]
        public string Reason { get; set; } = string.Empty;

        [Required(ErrorMessage = "El comentario es obligatorio.")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "El comentario debe tener entre 3 y 1000 caracteres.")]
        public string Comment { get; set; } = string.Empty;
    }
}