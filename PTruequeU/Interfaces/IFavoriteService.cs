using PTruequeU.DTOs.Listings;

namespace PTruequeU.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> ToggleFavoriteAsync(string userId, int listingId);
        Task<List<ListingResponseDto>> GetUserFavoritesAsync(string userId);
        Task<bool> IsFavoritedAsync(string userId, int listingId);
    }
}
