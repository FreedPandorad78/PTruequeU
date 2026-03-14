using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Profile
{
    public class UpdateProfileDto
    {
        [MaxLength(100)]
        public string? FullName { get; set; }

        [MaxLength(100)]
        public string? Program { get; set; }
    }
}
