using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 120 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "El programa no puede superar 120 caracteres.")]
        public string? ProgramName { get; set; }
    }
}