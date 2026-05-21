using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Admin;
using PTruequeU.Interfaces;
using PTruequeU.Models;
using PTruequeU.Models.Enums;

namespace PTruequeU.Services
{
    public class AdminModerationService : IAdminModerationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminModerationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ModerationActionResponseDto?> HideListing(string adminId, Guid listingId, HideListingRequestDto dto)
        {
            await EnsureIsAdmin(adminId);

            var listing = await _context.Listings
                .FirstOrDefaultAsync(l => l.Listing_id == listingId);

            if (listing == null)
            {
                return null;
            }

            if (listing.IsHidden)
            {
                return new ModerationActionResponseDto
                {
                    ModerationActionId = Guid.Empty,
                    AdminId = adminId,
                    ActionType = ModerationActionType.HideListing,
                    TargetId = listingId.ToString(),
                    Reason = dto.Reason.Trim(),
                    CreatedAt = DateTime.UtcNow,
                    WasApplied = false,
                    Message = "El listing ya estaba oculto."
                };
            }

            listing.IsHidden = true;
            listing.UpdatedAt = DateTime.UtcNow;

            var action = new ModerationAction
            {
                ModerationAction_Id = Guid.NewGuid(),
                Admin_Id = adminId,
                ActionType = ModerationActionType.HideListing,
                TargetId = listingId.ToString(),
                Reason = dto.Reason.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.ModerationActions.Add(action);
            await _context.SaveChangesAsync();

            return new ModerationActionResponseDto
            {
                ModerationActionId = action.ModerationAction_Id,
                AdminId = action.Admin_Id,
                ActionType = action.ActionType,
                TargetId = action.TargetId,
                Reason = action.Reason,
                CreatedAt = action.CreatedAt,
                WasApplied = true,
                Message = "Listing ocultado correctamente."
            };
        }

        public async Task<ModerationActionResponseDto?> SuspendUser(string adminId, string userId, SuspendUserRequestDto dto)
        {
            await EnsureIsAdmin(adminId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            if (user.IsSuspended)
            {
                return new ModerationActionResponseDto
                {
                    ModerationActionId = Guid.Empty,
                    AdminId = adminId,
                    ActionType = ModerationActionType.SuspendUser,
                    TargetId = userId,
                    Reason = dto.Reason.Trim(),
                    CreatedAt = DateTime.UtcNow,
                    WasApplied = false,
                    Message = "El usuario ya estaba suspendido."
                };
            }

            user.IsSuspended = true;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(" | ", updateResult.Errors.Select(e => e.Description)));
            }

            var action = new ModerationAction
            {
                ModerationAction_Id = Guid.NewGuid(),
                Admin_Id = adminId,
                ActionType = ModerationActionType.SuspendUser,
                TargetId = userId,
                Reason = dto.Reason.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.ModerationActions.Add(action);
            await _context.SaveChangesAsync();

            return new ModerationActionResponseDto
            {
                ModerationActionId = action.ModerationAction_Id,
                AdminId = action.Admin_Id,
                ActionType = action.ActionType,
                TargetId = action.TargetId,
                Reason = action.Reason,
                CreatedAt = action.CreatedAt,
                WasApplied = true,
                Message = "Usuario suspendido correctamente."
            };
        }

        public async Task<ModerationActionResponseDto?> UnsuspendUser(string adminId, string userId)
        {
            await EnsureIsAdmin(adminId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            if (!user.IsSuspended)
            {
                return new ModerationActionResponseDto
                {
                    ModerationActionId = Guid.Empty,
                    AdminId = adminId,
                    ActionType = ModerationActionType.SuspendUser,
                    TargetId = userId,
                    Reason = "Reactivación de cuenta",
                    CreatedAt = DateTime.UtcNow,
                    WasApplied = false,
                    Message = "El usuario no estaba suspendido."
                };
            }

            user.IsSuspended = false;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new InvalidOperationException(string.Join(" | ", updateResult.Errors.Select(e => e.Description)));

            var action = new ModerationAction
            {
                ModerationAction_Id = Guid.NewGuid(),
                Admin_Id = adminId,
                ActionType = ModerationActionType.SuspendUser,
                TargetId = userId,
                Reason = "Reactivación de cuenta",
                CreatedAt = DateTime.UtcNow
            };

            _context.ModerationActions.Add(action);
            await _context.SaveChangesAsync();

            return new ModerationActionResponseDto
            {
                ModerationActionId = action.ModerationAction_Id,
                AdminId = action.Admin_Id,
                ActionType = action.ActionType,
                TargetId = action.TargetId,
                Reason = action.Reason,
                CreatedAt = action.CreatedAt,
                WasApplied = true,
                Message = "Cuenta reactivada correctamente."
            };
        }

        public async Task<List<ModerationActionResponseDto>> GetAudit()
        {
            var actions = await _context.ModerationActions
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return actions.Select(a => new ModerationActionResponseDto
            {
                ModerationActionId = a.ModerationAction_Id,
                AdminId = a.Admin_Id,
                ActionType = a.ActionType,
                TargetId = a.TargetId,
                Reason = a.Reason,
                CreatedAt = a.CreatedAt,
                WasApplied = true,
                Message = "Registro de auditoría"
            }).ToList();
        }

        private async Task EnsureIsAdmin(string adminId)
        {
            var admin = await _userManager.FindByIdAsync(adminId);
            if (admin == null)
            {
                throw new UnauthorizedAccessException("Usuario administrador no válido.");
            }

            var isAdmin = await _userManager.IsInRoleAsync(admin, "Admin");
            if (!isAdmin)
            {
                throw new UnauthorizedAccessException("No autorizado. Se requiere rol Admin.");
            }
        }
    }
}