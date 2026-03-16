using System.ComponentModel.DataAnnotations;

namespace PTruequeU.Models
{
    public class Favorite
    {
        [Key]
        public Guid Favorite_Id { get; set; }

        public Guid Listing_Id { get; set; }

        public string User_Id { get; set; } = string.Empty;

        public Listing? Listing { get; set; }

        public ApplicationUser? User { get; set; }
    }
}