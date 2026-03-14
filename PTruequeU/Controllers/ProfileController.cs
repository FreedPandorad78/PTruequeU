using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Profile;
using PTruequeU.Models;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ProfileResponseDto>> GetProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(MapToDto(user));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<ProfileResponseDto>> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(MapToDto(user));
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<ActionResult<ProfileResponseDto>> UpdateMyProfile(UpdateProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (dto.FullName != null) user.FullName = dto.FullName;
            if (dto.Program != null) user.Program = dto.Program;

            await _userManager.UpdateAsync(user);

            return Ok(MapToDto(user));
        }

        private static ProfileResponseDto MapToDto(ApplicationUser user)
        {
            return new ProfileResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Program = user.Program,
                Rating = user.Rating,
                RatingCount = user.RatingCount,
                IsSuspended = user.IsSuspended,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
