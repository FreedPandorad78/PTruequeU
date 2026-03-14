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

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
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

            var listings = await _context.Listings
                .Include(l => l.User)
                .Include(l => l.Category)
                .Include(l => l.Images.OrderBy(i => i.DisplayOrder))
                .Include(l => l.Favorites)
                .Where(l => favoriteListingIds.Contains(l.Id) && !l.IsHidden)
                .ToListAsync();

            return listings.Select(l => new ListingResponseDto
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                Condition = l.Condition,
                Price = l.Price,
                Location = l.Location,
                State = l.State,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt,
                UserId = l.UserId,
                UserFullName = l.User?.FullName ?? string.Empty,
                CategoryId = l.CategoryId,
                CategoryName = l.Category?.Name ?? string.Empty,
                Images = l.Images?.Select(i => new ListingImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    DisplayOrder = i.DisplayOrder
                }).ToList() ?? new List<ListingImageDto>(),
                FavoriteCount = l.Favorites?.Count ?? 0
            }).ToList();
        }

        public async Task<bool> IsFavoritedAsync(string userId, int listingId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ListingId == listingId);
        }
    }
}
