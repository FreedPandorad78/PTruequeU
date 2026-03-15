using Microsoft.AspNetCore.Identity;

namespace PTruequeU.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsSuspended { get; set; } = false;
        public string? FullName { get; set; }
        public string? ProgramName { get; set; }
        public double Rating { get; set; } = 0;
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}