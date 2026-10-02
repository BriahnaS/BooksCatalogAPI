namespace BooksCatalogAPI.Models
{
    public class Ebook
    {
        public int EbookId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public bool IsAvailableOnKU { get; set; }
        public DateOnly PublicationDate { get; set; }
    }
}
