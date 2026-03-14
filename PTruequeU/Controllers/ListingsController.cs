using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Listings;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingsController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListingResponseDto>>> Search([FromQuery] ListingSearchDto search)
        {
            var listings = await _listingService.SearchAsync(search);
            return Ok(listings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingResponseDto>> GetById(int id)
        {
            var listing = await _listingService.GetByIdAsync(id);
            if (listing == null) return NotFound();
            return Ok(listing);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ListingResponseDto>> Create(CreateListingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var listing = await _listingService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = listing.Id }, listing);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ListingResponseDto>> Update(int id, UpdateListingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var listing = await _listingService.UpdateAsync(id, userId, dto);
            if (listing == null) return NotFound();
            return Ok(listing);
        }

        [Authorize]
        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ListingResponseDto>> UpdateState(int id, UpdateListingStateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var listing = await _listingService.UpdateStateAsync(id, userId, dto);
            if (listing == null) return BadRequest("Invalid state transition or listing not found.");
            return Ok(listing);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _listingService.DeleteAsync(id, userId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
