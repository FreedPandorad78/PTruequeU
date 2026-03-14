using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Reports;
using PTruequeU.Interfaces;
using PTruequeU.Models;

namespace PTruequeU.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReportResponseDto> CreateReportAsync(string reporterId, CreateReportDto dto)
        {
            var report = new Report
            {
                TargetType = dto.TargetType,
                ListingId = dto.ListingId,
                ReportedUserId = dto.ReportedUserId,
                Reason = dto.Reason,
                Comment = dto.Comment,
                ReporterId = reporterId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return await MapToDtoAsync(report);
        }

        public async Task<List<ReportResponseDto>> GetAllReportsAsync()
        {
            var reports = await _context.Reports
                .Include(r => r.Reporter)
                .Include(r => r.Listing)
                .Include(r => r.ReportedUser)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return reports.Select(r => MapToDto(r)).ToList();
        }

        public async Task<ReportResponseDto?> ResolveReportAsync(int reportId)
        {
            var report = await _context.Reports
                .Include(r => r.Reporter)
                .Include(r => r.Listing)
                .Include(r => r.ReportedUser)
                .FirstOrDefaultAsync(r => r.Id == reportId);

            if (report == null) return null;

            report.IsResolved = true;
            await _context.SaveChangesAsync();

            return MapToDto(report);
        }

        private async Task<ReportResponseDto> MapToDtoAsync(Report report)
        {
            var reporter = await _context.Users.FindAsync(report.ReporterId);
            var listing = report.ListingId.HasValue ? await _context.Listings.FindAsync(report.ListingId) : null;
            var reportedUser = report.ReportedUserId != null ? await _context.Users.FindAsync(report.ReportedUserId) : null;

            return new ReportResponseDto
            {
                Id = report.Id,
                TargetType = report.TargetType,
                ListingId = report.ListingId,
                ListingTitle = listing?.Title,
                ReportedUserId = report.ReportedUserId,
                ReportedUserName = reportedUser?.FullName,
                Reason = report.Reason,
                Comment = report.Comment,
                IsResolved = report.IsResolved,
                CreatedAt = report.CreatedAt,
                ReporterId = report.ReporterId,
                ReporterName = reporter?.FullName ?? string.Empty
            };
        }

        private static ReportResponseDto MapToDto(Report report)
        {
            return new ReportResponseDto
            {
                Id = report.Id,
                TargetType = report.TargetType,
                ListingId = report.ListingId,
                ListingTitle = report.Listing?.Title,
                ReportedUserId = report.ReportedUserId,
                ReportedUserName = report.ReportedUser?.FullName,
                Reason = report.Reason,
                Comment = report.Comment,
                IsResolved = report.IsResolved,
                CreatedAt = report.CreatedAt,
                ReporterId = report.ReporterId,
                ReporterName = report.Reporter?.FullName ?? string.Empty
            };
        }
    }
}
