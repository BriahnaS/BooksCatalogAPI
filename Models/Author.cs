using BooksCatalogAPI.Models.JoinTables;

namespace BooksCatalogAPI.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
