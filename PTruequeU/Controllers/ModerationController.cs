using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Moderation;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ModerationController : ControllerBase
    {
        private readonly IModerationService _moderationService;

        public ModerationController(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        [HttpPost("hide-listing")]
        public async Task<ActionResult> HideListing(HideListingDto dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _moderationService.HideListingAsync(adminId, dto);
            if (!result) return NotFound();
            return Ok(new { Message = "Listing hidden successfully." });
        }

        [HttpPost("unhide-listing/{listingId}")]
        public async Task<ActionResult> UnhideListing(int listingId)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _moderationService.UnhideListingAsync(adminId, listingId);
            if (!result) return NotFound();
            return Ok(new { Message = "Listing unhidden successfully." });
        }

        [HttpPost("suspend-user")]
        public async Task<ActionResult> SuspendUser(SuspendUserDto dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _moderationService.SuspendUserAsync(adminId, dto);
            if (!result) return NotFound();
            return Ok(new { Message = "User suspended successfully." });
        }

        [HttpPost("unsuspend-user/{userId}")]
        public async Task<ActionResult> UnsuspendUser(string userId)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _moderationService.UnsuspendUserAsync(adminId, userId);
            if (!result) return NotFound();
            return Ok(new { Message = "User unsuspended successfully." });
        }

        [HttpGet("log")]
        public async Task<ActionResult<List<ModerationActionDto>>> GetModerationLog()
        {
            var log = await _moderationService.GetModerationLogAsync();
            return Ok(log);
        }
    }
}
