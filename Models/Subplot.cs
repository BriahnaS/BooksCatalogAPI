using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Subplot
    {
        public int SubplotId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<GenreSubplot> GenreSubplots { get; set; } = new List<GenreSubplot>();

        public ICollection<BookSubplot> BookSubplots { get; set; } = new List<BookSubplot>();
    }
}
