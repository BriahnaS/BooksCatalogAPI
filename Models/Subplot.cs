using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Subplot
    {
        public int SubplotId { get; set; }
        public string Name { get; set; }

        public ICollection<GenreSubplot> GenreSubplots { get; set; }
    }
}
