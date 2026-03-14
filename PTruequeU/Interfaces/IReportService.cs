using PTruequeU.DTOs.Reports;

namespace PTruequeU.Interfaces
{
    public interface IReportService
    {
        Task<ReportResponseDto> CreateReportAsync(string reporterId, CreateReportDto dto);
        Task<List<ReportResponseDto>> GetAllReportsAsync();
        Task<ReportResponseDto?> ResolveReportAsync(int reportId);
    }
}
