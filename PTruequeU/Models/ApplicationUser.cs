using Microsoft.AspNetCore.Identity;

namespace PTruequeU.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsSuspended { get; set; } = false;

        // Perfil publico basico 
        public string FullName { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public double Rating { get; set; } = 0;

        // Navegaciones (minimas por ahora)
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}