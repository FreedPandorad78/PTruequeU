using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Chat
{
    public class CreateChatMessageDto
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;
    }
}
