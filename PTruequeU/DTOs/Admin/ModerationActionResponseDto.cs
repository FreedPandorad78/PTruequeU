using PTruequeU.Models.Enums;

namespace PTruequeU.DTOs.Admin
{
    public class ModerationActionResponseDto
    {
        public Guid ModerationActionId { get; set; }
        public string AdminId { get; set; } = string.Empty;
        public ModerationActionType ActionType { get; set; }
        public string TargetId { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public bool WasApplied { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}