using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PTruequeU.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Program { get; set; } = string.Empty;

        public double Rating { get; set; } = 0;

        public int RatingCount { get; set; } = 0;

        public bool IsSuspended { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
        public ICollection<Report> ReportsFiled { get; set; } = new List<Report>();
    }
}
