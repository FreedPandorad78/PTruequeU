using PTruequeU.DTOs.Admin;

namespace PTruequeU.Interfaces
{
    public interface IAdminModerationService
    {
        Task<ModerationActionResponseDto?> HideListing(string adminId, Guid listingId, HideListingRequestDto dto);
        Task<ModerationActionResponseDto?> SuspendUser(string adminId, string userId, SuspendUserRequestDto dto);
        Task<List<ModerationActionResponseDto>> GetAudit();
    }
}