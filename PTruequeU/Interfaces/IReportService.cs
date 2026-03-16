using PTruequeU.DTOs.Admin;
using PTruequeU.DTOs.Reports;

namespace PTruequeU.Interfaces
{
    public interface IReportService
    {
        Task<ReportResponseDto?> CreateListingReport(string reporterId, Guid listingId, CreateListingReportDto dto);
        Task<ReportResponseDto?> CreateUserReport(string reporterId, string reportedUserId, CreateUserReportDto dto);
        Task<List<AdminReportResponseDto>> GetAllReports();
    }
}