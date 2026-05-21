using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Admin;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminModerationService _adminModerationService;
        private readonly IReportService _reportService;

        public AdminController(
            IAdminModerationService adminModerationService,
            IReportService reportService)
        {
            _adminModerationService = adminModerationService;
            _reportService = reportService;
        }

        [HttpPatch("listings/{id:guid}/hide")]
        public async Task<IActionResult> HideListing(Guid id, [FromBody] HideListingRequestDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var result = await _adminModerationService.HideListing(adminId, id, dto);
                if (result == null)
                    return NotFound("La publicación no existe.");

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPatch("listings/{id:guid}/show")]
        public async Task<IActionResult> ShowListing(Guid id)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var result = await _adminModerationService.ShowListing(adminId, id);
                if (result == null)
                    return NotFound("La publicación no existe.");

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPatch("users/{id}/suspend")]
        public async Task<IActionResult> SuspendUser(string id, [FromBody] SuspendUserRequestDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var result = await _adminModerationService.SuspendUser(adminId, id, dto);
                if (result == null)
                    return NotFound("El usuario no existe.");

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("users/{id}/unsuspend")]
        public async Task<IActionResult> UnsuspendUser(string id)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var result = await _adminModerationService.UnsuspendUser(adminId, id);
                if (result == null)
                    return NotFound("El usuario no existe.");

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminModerationService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _reportService.GetAllReports();
            return Ok(reports);
        }

        [HttpGet("audit")]
        public async Task<IActionResult> GetAudit()
        {
            var audit = await _adminModerationService.GetAudit();
            return Ok(audit);
        }
    }
}