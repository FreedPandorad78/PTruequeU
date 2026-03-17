using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Admin;
using PTruequeU.DTOs.Reports;
using PTruequeU.Interfaces;
using PTruequeU.Models;
using PTruequeU.Models.Enums;

namespace PTruequeU.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ReportResponseDto?> CreateListingReport(string reporterId, Guid listingId, CreateListingReportDto dto)
        {
            var reporter = await _userManager.FindByIdAsync(reporterId);
            if (reporter == null)
            {
                throw new UnauthorizedAccessException("Usuario autenticado no válido.");
            }

            var listing = await _context.Listings
                .FirstOrDefaultAsync(l => l.Listing_id == listingId);

            if (listing == null)
            {
                return null;
            }

            var report = new Report
            {
                Report_Id = new Guid("f12803f2-f343-4d39-97b0-8703e8e33545"),
                Reporter_Id = reporterId,
                ReportedListing_Id = listingId,
                ReportedUser_Id = null,
                TargetType = ReportTargetType.Listing,
                Reason = dto.Reason.Trim(),
                Comment = dto.Comment.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return MapReport(report);
        }

        public async Task<ReportResponseDto?> CreateUserReport(string reporterId, string reportedUserId, CreateUserReportDto dto)
        {
            var reporter = await _userManager.FindByIdAsync(reporterId);
            if (reporter == null)
            {
                throw new UnauthorizedAccessException("Usuario autenticado no válido.");
            }

            var reportedUser = await _userManager.FindByIdAsync(reportedUserId);
            if (reportedUser == null)
            {
                return null;
            }

            var report = new Report
            {
                Report_Id = new Guid("139dd3f9-aeb9-4aa5-85d2-56199149a555"),
                Reporter_Id = reporterId,
                ReportedUser_Id = reportedUserId,
                ReportedListing_Id = null,
                TargetType = ReportTargetType.User,
                Reason = dto.Reason.Trim(),
                Comment = dto.Comment.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return MapReport(report);
        }

        public async Task<List<AdminReportResponseDto>> GetAllReports()
        {
            var reports = await _context.Reports
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return reports.Select(r => new AdminReportResponseDto
            {
                ReportId = r.Report_Id,
                ReporterId = r.Reporter_Id,
                TargetType = r.TargetType,
                ReportedUserId = r.ReportedUser_Id,
                ReportedListingId = r.ReportedListing_Id,
                Reason = r.Reason,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        private static ReportResponseDto MapReport(Report report)
        {
            return new ReportResponseDto
            {
                ReportId = report.Report_Id,
                ReporterId = report.Reporter_Id,
                TargetType = report.TargetType,
                ReportedUserId = report.ReportedUser_Id,
                ReportedListingId = report.ReportedListing_Id,
                Reason = report.Reason,
                Comment = report.Comment,
                CreatedAt = report.CreatedAt
            };
        }
    }
}