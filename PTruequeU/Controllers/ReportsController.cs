using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Reports;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<ActionResult<ReportResponseDto>> CreateReport(CreateReportDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var report = await _reportService.CreateReportAsync(userId, dto);
            return CreatedAtAction(nameof(CreateReport), new { id = report.Id }, report);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ReportResponseDto>>> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpPatch("{id}/resolve")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReportResponseDto>> ResolveReport(int id)
        {
            var report = await _reportService.ResolveReportAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }
    }
}
