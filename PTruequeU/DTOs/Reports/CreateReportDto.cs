using PTruequeU.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Reports
{
    public class CreateReportDto
    {
        [Required]
        public ReportTargetType TargetType { get; set; }

        public int? ListingId { get; set; }

        public string? ReportedUserId { get; set; }

        [Required]
        public ReportReason Reason { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}
