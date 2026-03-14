using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Reports
{
    public class ReportResponseDto
    {
        public int Id { get; set; }
        public ReportTargetType TargetType { get; set; }
        public int? ListingId { get; set; }
        public string? ListingTitle { get; set; }
        public string? ReportedUserId { get; set; }
        public string? ReportedUserName { get; set; }
        public ReportReason Reason { get; set; }
        public string? Comment { get; set; }
        public bool IsResolved { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ReporterId { get; set; } = string.Empty;
        public string ReporterName { get; set; } = string.Empty;
    }
}
