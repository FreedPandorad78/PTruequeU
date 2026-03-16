using PTruequeU.Models.Enums;

namespace PTruequeU.Models
{
    public class Report
    {
        public Guid Report_Id { get; set; }

        public string Reporter_Id { get; set; } = string.Empty;
        public ApplicationUser? Reporter { get; set; }

        public string? ReportedUser_Id { get; set; }
        public ApplicationUser? ReportedUser { get; set; }

        public Guid? ReportedListing_Id { get; set; }
        public Listing? ReportedListing { get; set; }

        public ReportTargetType TargetType { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}