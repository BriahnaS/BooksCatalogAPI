namespace BooksCatalogAPI.Models.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string AuthorList => string.Join(", ", Authors);
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
    }
}
