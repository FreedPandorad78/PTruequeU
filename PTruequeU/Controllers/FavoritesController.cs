using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Listings;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost("{listingId}")]
        public async Task<ActionResult> ToggleFavorite(int listingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isFavorited = await _favoriteService.ToggleFavoriteAsync(userId, listingId);
            return Ok(new { IsFavorited = isFavorited });
        }

        [HttpGet]
        public async Task<ActionResult<List<ListingResponseDto>>> GetFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var favorites = await _favoriteService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }

        [HttpGet("{listingId}/check")]
        public async Task<ActionResult> CheckFavorite(int listingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isFavorited = await _favoriteService.IsFavoritedAsync(userId, listingId);
            return Ok(new { IsFavorited = isFavorited });
        }
    }
}
