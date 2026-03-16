using PTruequeU.DTOs.Favorites;

namespace PTruequeU.Interfaces
{
    public interface IFavoriteService
    {
        Task<FavoriteResponseDto?> AddFavorite(string currentUserId, Guid listingId);
        Task<bool> RemoveFavorite(string currentUserId, Guid listingId);
        Task<List<FavoriteResponseDto>> GetMyFavorites(string currentUserId);
    }
}