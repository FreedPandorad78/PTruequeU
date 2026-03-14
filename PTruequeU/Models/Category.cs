using System.ComponentModel.DataAnnotations;

namespace PTruequeU.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        // Navigation
        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
