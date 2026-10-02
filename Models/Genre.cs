using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public ICollection<GenreSubplot> GenreSuplots { get; set; }
        public ICollection<GenreTrope> GenreTropes { get; set; }
    }
}
