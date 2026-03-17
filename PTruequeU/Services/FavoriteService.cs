using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Favorites;
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

        public async Task<FavoriteResponseDto?> AddFavorite(string currentUserId, Guid listingId)
        {
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                throw new UnauthorizedAccessException("Usuario no autenticado.");
            }

            var listing = await _context.Listings
                .Include(l => l.Images.OrderBy(i => i.DisplayOrder))
                .FirstOrDefaultAsync(l => l.Listing_id == listingId);

            if (listing == null)
            {
                return null;
            }

            // Si está oculto, para usuario normal se comporta como no disponible
            if (listing.IsHidden)
            {
                return null;
            }

            var alreadyExists = await _context.Favorites
                .AnyAsync(f => f.User_Id == currentUserId && f.Listing_Id == listingId);

            if (alreadyExists)
            {
                throw new InvalidOperationException("Este listing ya está en favoritos.");
            }

            var favorite = new Favorite
            {
                Favorite_Id = new Guid("a90991ea-a5c5-461a-b53d-f854b8c6910a"),
                Listing_Id = listingId,
                User_Id = currentUserId
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return MapToDto(favorite, listing);
        }

        public async Task<bool> RemoveFavorite(string currentUserId, Guid listingId)
        {
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                throw new UnauthorizedAccessException("Usuario no autenticado.");
            }

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.User_Id == currentUserId && f.Listing_Id == listingId);

            if (favorite == null)
            {
                return false;
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<FavoriteResponseDto>> GetMyFavorites(string currentUserId)
        {
            if (string.IsNullOrWhiteSpace(currentUserId))
                throw new UnauthorizedAccessException("Usuario no autenticado.");

            var favorites = await _context.Favorites
                .AsNoTracking()
                .Include(f => f.Listing)!
                    .ThenInclude(l => l.Images)
                .Where(f => f.User_Id == currentUserId && f.Listing != null)
                .OrderByDescending(f => f.Favorite_Id)
                .ToListAsync();

            return favorites
                .Where(f => f.Listing != null)
                .Select(f => MapToDto(f, f.Listing!))
                .ToList();
        }

        private static FavoriteResponseDto MapToDto(Favorite favorite, Listing listing)
        {
            return new FavoriteResponseDto
            {
                FavoriteId = favorite.Favorite_Id,
                ListingId = listing.Listing_id,
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,
                Location = listing.Location,
                UserId = listing.User_Id,
                CategoryId = listing.Category_Id,
                IsHidden = listing.IsHidden,
                CreatedAt = listing.CreatedAt,
                UpdatedAt = listing.UpdatedAt,
                Images = listing.Images
                    .OrderBy(i => i.DisplayOrder)
                    .Select(i => i.ImageUrl)
                    .ToList()
            };
        }
    }
}