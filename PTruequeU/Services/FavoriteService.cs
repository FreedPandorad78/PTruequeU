using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Listings;
using PTruequeU.Interfaces;
using PTruequeU.Models;

namespace PTruequeU.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IListingService _listingService;

        public FavoriteService(ApplicationDbContext context, IListingService listingService)
        {
            _context = context;
            _listingService = listingService;
        }

        public async Task<bool> ToggleFavoriteAsync(string userId, int listingId)
        {
            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ListingId == listingId);

            if (existing != null)
            {
                _context.Favorites.Remove(existing);
                await _context.SaveChangesAsync();
                return false; // Unfavorited
            }

            _context.Favorites.Add(new Favorite
            {
                UserId = userId,
                ListingId = listingId,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
            return true; // Favorited
        }

        public async Task<List<ListingResponseDto>> GetUserFavoritesAsync(string userId)
        {
            var favoriteListingIds = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.ListingId)
                .ToListAsync();

            var results = new List<ListingResponseDto>();
            foreach (var listingId in favoriteListingIds)
            {
                var listing = await _listingService.GetByIdAsync(listingId);
                if (listing != null) results.Add(listing);
            }
            return results;
        }

        public async Task<bool> IsFavoritedAsync(string userId, int listingId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ListingId == listingId);
        }
    }
}
