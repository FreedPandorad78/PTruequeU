namespace PTruequeU.DTOs.Admin
{
    public class AdminUserResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? ProgramName { get; set; }
        public double Rating { get; set; }
        public bool IsSuspended { get; set; }
    }
}
