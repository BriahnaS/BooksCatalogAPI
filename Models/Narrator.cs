using BooksCatalogAPI.Models.JoinTables;

namespace BooksCatalogAPI.Models
{
    public class Narrator
    {
        public int NarratorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookNarrator> BookNarrators { get; set; }
    }
}
