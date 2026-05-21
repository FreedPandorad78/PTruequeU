using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Admin
{
    public class CreateUserRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }
        public string? ProgramName { get; set; }

        [Required]
        [RegularExpression("^(User|Admin)$", ErrorMessage = "El rol debe ser 'User' o 'Admin'.")]
        public string Role { get; set; } = "User";
    }
}
