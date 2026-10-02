using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Audiobook
    {
        public int AudiobookId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public bool IsSingleNarration { get; set; }
        public bool IsDuetNarration { get; set; }
        public bool IsDualNarration { get; set; }
        public bool IsGraphicAudio { get; set; }
        public DateOnly PublicationDate { get; set; }

        public ICollection<BookNarrator> BookNarrators { get; set; }
    }
}
