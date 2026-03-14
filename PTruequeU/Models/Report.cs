using PTruequeU.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PTruequeU.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public ReportTargetType TargetType { get; set; }

        public int? ListingId { get; set; }
        public Listing? Listing { get; set; }

        public string? ReportedUserId { get; set; }
        public ApplicationUser? ReportedUser { get; set; }

        [Required]
        public ReportReason Reason { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public bool IsResolved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Who filed the report
        [Required]
        public string ReporterId { get; set; } = string.Empty;
        public ApplicationUser Reporter { get; set; } = null!;
    }
}
