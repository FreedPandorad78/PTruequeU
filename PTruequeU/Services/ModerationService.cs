using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Moderation;
using PTruequeU.Interfaces;
using PTruequeU.Models;

namespace PTruequeU.Services
{
    public class ModerationService : IModerationService
    {
        private readonly ApplicationDbContext _context;

        public ModerationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HideListingAsync(string adminId, HideListingDto dto)
        {
            var listing = await _context.Listings.FindAsync(dto.ListingId);
            if (listing == null) return false;

            listing.IsHidden = true;
            
            _context.ModerationActions.Add(new ModerationAction
            {
                ActionType = "HideListing",
                Reason = dto.Reason,
                ListingId = dto.ListingId,
                AdminId = adminId,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnhideListingAsync(string adminId, int listingId)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null) return false;

            listing.IsHidden = false;

            _context.ModerationActions.Add(new ModerationAction
            {
                ActionType = "UnhideListing",
                ListingId = listingId,
                AdminId = adminId,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SuspendUserAsync(string adminId, SuspendUserDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null) return false;

            user.IsSuspended = true;

            _context.ModerationActions.Add(new ModerationAction
            {
                ActionType = "SuspendUser",
                Reason = dto.Reason,
                TargetUserId = dto.UserId,
                AdminId = adminId,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnsuspendUserAsync(string adminId, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsSuspended = false;

            _context.ModerationActions.Add(new ModerationAction
            {
                ActionType = "UnsuspendUser",
                TargetUserId = userId,
                AdminId = adminId,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ModerationActionDto>> GetModerationLogAsync()
        {
            return await _context.ModerationActions
                .Include(m => m.Admin)
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new ModerationActionDto
                {
                    Id = m.Id,
                    ActionType = m.ActionType,
                    Reason = m.Reason,
                    ListingId = m.ListingId,
                    TargetUserId = m.TargetUserId,
                    AdminId = m.AdminId,
                    AdminName = m.Admin.FullName,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();
        }
    }
}
