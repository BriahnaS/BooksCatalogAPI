using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }

        public int EditionTypeId { get; set; }
        public EditionType EditionType { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int Length { get; set; }
        public string Description { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string? CoverImageUrl { get; set; }
        public int? ISBN { get; set; }


        public ICollection<Audiobook> Audiobooks { get; set; }
        public ICollection<Ebook> Ebooks { get; set; }
        public ICollection<PhysicalBook> PhysicalBooks { get; set; }

        //Join tables
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookSubplot> BookSubplots { get; set; }
        public ICollection<BookTrope> BookTropes { get; set; }



    }
}
