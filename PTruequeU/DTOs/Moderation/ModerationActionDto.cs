namespace PTruequeU.DTOs.Moderation
{
    public class ModerationActionDto
    {
        public int Id { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public int? ListingId { get; set; }
        public string? TargetUserId { get; set; }
        public string AdminId { get; set; } = string.Empty;
        public string AdminName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
