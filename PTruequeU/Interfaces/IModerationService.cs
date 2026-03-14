using PTruequeU.DTOs.Moderation;

namespace PTruequeU.Interfaces
{
    public interface IModerationService
    {
        Task<bool> HideListingAsync(string adminId, HideListingDto dto);
        Task<bool> UnhideListingAsync(string adminId, int listingId);
        Task<bool> SuspendUserAsync(string adminId, SuspendUserDto dto);
        Task<bool> UnsuspendUserAsync(string adminId, string userId);
        Task<List<ModerationActionDto>> GetModerationLogAsync();
    }
}
