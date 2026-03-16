using PTruequeU.Models.Enums;

namespace PTruequeU.Models
{
    public class ModerationAction
    {
        public Guid ModerationAction_Id { get; set; }

        public string Admin_Id { get; set; } = string.Empty;
        public ApplicationUser? Admin { get; set; }

        public ModerationActionType ActionType { get; set; }

        // Se guarda como string para soportar tanto Guid de Listing como string de User
        public string TargetId { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}