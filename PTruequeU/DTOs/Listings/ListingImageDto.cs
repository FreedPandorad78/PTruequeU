namespace PTruequeU.DTOs.Listings
{
    public class ListingImageDto
    {
        public Guid ListingImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}