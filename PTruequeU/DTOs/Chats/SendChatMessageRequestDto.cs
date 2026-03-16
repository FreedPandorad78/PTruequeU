using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Chats
{
    public class SendChatMessageRequestDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;
    }
}