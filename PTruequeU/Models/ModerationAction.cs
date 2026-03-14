using System.ComponentModel.DataAnnotations;

namespace PTruequeU.Models
{
    public class ModerationAction
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ActionType { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Reason { get; set; }

        public int? ListingId { get; set; }
        public Listing? Listing { get; set; }

        public string? TargetUserId { get; set; }
        public ApplicationUser? TargetUser { get; set; }

        [Required]
        public string AdminId { get; set; } = string.Empty;
        public ApplicationUser Admin { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
