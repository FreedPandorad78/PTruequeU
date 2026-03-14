using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Listings;
using PTruequeU.Interfaces;
using PTruequeU.Models;
using PTruequeU.Models.Enums;

namespace PTruequeU.Services
{
    public class ListingService : IListingService
    {
        private readonly ApplicationDbContext _context;

        public ListingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ListingResponseDto> CreateAsync(string userId, CreateListingDto dto)
        {
            var listing = new Listing
            {
                Title = dto.Title,
                Description = dto.Description,
                Condition = dto.Condition,
                Price = dto.Price,
                Location = dto.Location,
                CategoryId = dto.CategoryId,
                UserId = userId,
                State = ListingState.Available,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            // Add images
            for (int i = 0; i < dto.ImageUrls.Count; i++)
            {
                _context.ListingImages.Add(new ListingImage
                {
                    ListingId = listing.Id,
                    ImageUrl = dto.ImageUrls[i],
                    DisplayOrder = i
                });
            }
            await _context.SaveChangesAsync();

            return (await GetByIdAsync(listing.Id))!;
        }

        public async Task<ListingResponseDto?> GetByIdAsync(int id)
        {
            var listing = await _context.Listings
                .Include(l => l.User)
                .Include(l => l.Category)
                .Include(l => l.Images.OrderBy(i => i.DisplayOrder))
                .Include(l => l.Favorites)
                .Where(l => !l.IsHidden)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (listing == null) return null;

            return MapToDto(listing);
        }

        public async Task<List<ListingResponseDto>> SearchAsync(ListingSearchDto search)
        {
            var query = _context.Listings
                .Include(l => l.User)
                .Include(l => l.Category)
                .Include(l => l.Images.OrderBy(i => i.DisplayOrder))
                .Include(l => l.Favorites)
                .Where(l => !l.IsHidden)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();
                query = query.Where(l => l.Title.ToLower().Contains(keyword) || l.Description.ToLower().Contains(keyword));
            }

            if (search.CategoryId.HasValue)
                query = query.Where(l => l.CategoryId == search.CategoryId.Value);

            if (search.MinPrice.HasValue)
                query = query.Where(l => l.Price >= search.MinPrice.Value);

            if (search.MaxPrice.HasValue)
                query = query.Where(l => l.Price <= search.MaxPrice.Value);

            if (search.Condition.HasValue)
                query = query.Where(l => l.Condition == search.Condition.Value);

            if (search.State.HasValue)
                query = query.Where(l => l.State == search.State.Value);

            if (search.PostedAfter.HasValue)
                query = query.Where(l => l.CreatedAt >= search.PostedAfter.Value);

            if (search.PostedBefore.HasValue)
                query = query.Where(l => l.CreatedAt <= search.PostedBefore.Value);

            var listings = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip((search.Page - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            return listings.Select(MapToDto).ToList();
        }

        public async Task<ListingResponseDto?> UpdateAsync(int id, string userId, UpdateListingDto dto)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null || listing.UserId != userId) return null;

            if (dto.Title != null) listing.Title = dto.Title;
            if (dto.Description != null) listing.Description = dto.Description;
            if (dto.Condition.HasValue) listing.Condition = dto.Condition.Value;
            if (dto.Price.HasValue) listing.Price = dto.Price.Value;
            if (dto.Location != null) listing.Location = dto.Location;
            if (dto.CategoryId.HasValue) listing.CategoryId = dto.CategoryId.Value;
            listing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<ListingResponseDto?> UpdateStateAsync(int id, string userId, UpdateListingStateDto dto)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null || listing.UserId != userId) return null;

            // Sold cannot return to Available
            if (listing.State == ListingState.Sold && dto.State == ListingState.Available)
                return null;

            listing.State = dto.State;
            listing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null || listing.UserId != userId) return false;

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();
            return true;
        }

        private static ListingResponseDto MapToDto(Listing listing)
        {
            return new ListingResponseDto
            {
                Id = listing.Id,
                Title = listing.Title,
                Description = listing.Description,
                Condition = listing.Condition,
                Price = listing.Price,
                Location = listing.Location,
                State = listing.State,
                CreatedAt = listing.CreatedAt,
                UpdatedAt = listing.UpdatedAt,
                UserId = listing.UserId,
                UserFullName = listing.User?.FullName ?? string.Empty,
                CategoryId = listing.CategoryId,
                CategoryName = listing.Category?.Name ?? string.Empty,
                Images = listing.Images?.Select(i => new ListingImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    DisplayOrder = i.DisplayOrder
                }).ToList() ?? new List<ListingImageDto>(),
                FavoriteCount = listing.Favorites?.Count ?? 0
            };
        }
    }
}
