using PTruequeU.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PTruequeU.DTOs.Listings
{
    public class UpdateListingStateDto
    {
        [Required]
        public ListingState State { get; set; }
    }
}
