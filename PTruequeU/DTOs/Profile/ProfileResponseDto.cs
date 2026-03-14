namespace PTruequeU.DTOs.Profile
{
    public class ProfileResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int RatingCount { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
