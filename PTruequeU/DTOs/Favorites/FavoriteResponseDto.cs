namespace PTruequeU.DTOs.Favorites
{
    public class FavoriteResponseDto
    {
        public Guid FavoriteId { get; set; }
        public Guid ListingId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }

        public bool IsHidden { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<string> Images { get; set; } = new();
    }
}