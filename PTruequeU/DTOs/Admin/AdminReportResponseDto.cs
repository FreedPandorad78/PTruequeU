using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Admin
{
    public class AdminReportResponseDto
    {
        public Guid ReportId { get; set; }
        public string ReporterId { get; set; } = string.Empty;

        public ReportTargetType TargetType { get; set; }

        public string? ReportedUserId { get; set; }
        public Guid? ReportedListingId { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}