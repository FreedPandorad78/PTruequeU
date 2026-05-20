namespace PTruequeU.DTOs.Users
{
    public class UserPublicProfileDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public double Rating { get; set; }
    }
}
