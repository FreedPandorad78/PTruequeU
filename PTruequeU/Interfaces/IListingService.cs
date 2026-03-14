using PTruequeU.DTOs.Listings;

namespace PTruequeU.Interfaces
{
    public interface IListingService
    {
        Task<ListingResponseDto> CreateAsync(string userId, CreateListingDto dto);
        Task<ListingResponseDto?> GetByIdAsync(int id);
        Task<List<ListingResponseDto>> SearchAsync(ListingSearchDto search);
        Task<ListingResponseDto?> UpdateAsync(int id, string userId, UpdateListingDto dto);
        Task<ListingResponseDto?> UpdateStateAsync(int id, string userId, UpdateListingStateDto dto);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
