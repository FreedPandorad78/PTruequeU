using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Listings;
using PTruequeU.Interfaces;
using PTruequeU.Models;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ListingsController(IListingService listingService, UserManager<ApplicationUser> userManager)
        {
            _listingService = listingService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListingResponseDto>>> GetAll([FromQuery] ListingSearchDto search)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var role = User.FindFirstValue(ClaimTypes.Role);
            var isAdmin = role == "Admin";

            var result = await _listingService.Search(search, userId, isAdmin);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ListingResponseDto>> GetById(Guid id)
        {
            var listing = await _listingService.GetById(id);
            if (listing == null) return NotFound();
            return Ok(listing);
        }

        // Crear listing (requiere usuario autenticado y no suspendido).
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ListingResponseDto>> Create([FromBody] CreateListingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null && user.IsSuspended)
                return Forbid();

            var listing = await _listingService.Create(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = listing.ListingId }, listing);
        }

        // Editar listing (solo owner, validado en service).
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ListingResponseDto>> Update(Guid id, [FromBody] UpdateListingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var listing = await _listingService.Update(id, userId, dto);
            if (listing == null) return NotFound();

            return Ok(listing);
        }

        // Cambiar estado del listing (solo owner, validado en service).
        [Authorize]
        [HttpPatch("{id:guid}/state")]
        public async Task<ActionResult<ListingResponseDto>> UpdateState(Guid id, [FromBody] UpdateListingStateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var listing = await _listingService.UpdateState(id, userId, dto);
            if (listing == null) return BadRequest("Transición de estado inválida, publicación no encontrada o no eres el propietario.");

            return Ok(listing);
        }

        // Eliminar listing (solo owner).
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _listingService.Delete(id, userId);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}