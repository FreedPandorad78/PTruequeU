using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Reports;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize]
        [HttpPost("listings/{listingId:guid}")]
        public async Task<IActionResult> CreateListingReport(Guid listingId, [FromBody] CreateListingReportDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var reporterId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var report = await _reportService.CreateListingReport(reporterId, listingId, dto);
                if (report == null)
                    return NotFound("La publicación no existe.");

                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("users/{userId}")]
        public async Task<IActionResult> CreateUserReport(string userId, [FromBody] CreateUserReportDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var reporterId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var report = await _reportService.CreateUserReport(reporterId, userId, dto);
                if (report == null)
                    return NotFound("El usuario no existe.");

                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}