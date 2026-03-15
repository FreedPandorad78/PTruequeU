using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Chats
{
    public class StartChatRequestDto
    {
        [Required]
        public Guid ListingId { get; set; }
    }
}