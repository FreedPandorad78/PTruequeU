using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Users;
using PTruequeU.Models;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPublicProfileDto>> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(new UserPublicProfileDto
            {
                Id = user.Id,
                FullName = user.FullName ?? string.Empty,
                ProgramName = user.ProgramName ?? string.Empty,
                Rating = user.Rating
            });
        }
    }
}
