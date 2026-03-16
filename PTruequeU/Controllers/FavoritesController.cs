using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("favorites")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost("{listingId:guid}")]
        public async Task<IActionResult> AddFavorite(Guid listingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var result = await _favoriteService.AddFavorite(userId!, listingId);

                if (result == null)
                {
                    return NotFound(new { message = "Listing no encontrado o no disponible." });
                }

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{listingId:guid}")]
        public async Task<IActionResult> RemoveFavorite(Guid listingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var removed = await _favoriteService.RemoveFavorite(userId!, listingId);

                if (!removed)
                {
                    return NotFound(new { message = "El favorito no existe para este usuario." });
                }

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var favorites = await _favoriteService.GetMyFavorites(userId!);
                return Ok(favorites);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}