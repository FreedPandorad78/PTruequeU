using PTruequeU.DTOs.Listings;

namespace PTruequeU.Interfaces
{
    public interface IListingService
    {
        Task<ListingResponseDto> Create(string userId, CreateListingDto dto);
        Task<ListingResponseDto?> GetById(Guid id);
        Task<List<ListingResponseDto>> Search(ListingSearchDto search);
        Task<ListingResponseDto?> Update(Guid id, string userId, UpdateListingDto dto);
        Task<ListingResponseDto?> UpdateState(Guid id, string userId, UpdateListingStateDto dto);
        Task<bool> Delete(Guid id, string userId);
    }
}