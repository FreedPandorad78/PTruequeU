using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Moderation
{
    public class SuspendUserDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Reason { get; set; }
    }
}
