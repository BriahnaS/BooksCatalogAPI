namespace BooksCatalogAPI.Models.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
    }
}
