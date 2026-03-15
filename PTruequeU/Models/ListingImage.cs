namespace PTruequeU.Models
{
    public class ListingImage
    {
        public Guid ListingImage_Id { get; set; }

        public Guid Listing_Id { get; set; }
        public Listing? Listing { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; } = 0;
    }
}