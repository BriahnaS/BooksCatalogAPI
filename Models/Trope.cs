using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Trope
    {
        public int TropeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<GenreTrope> GenreTropes { get; set; } = new List<GenreTrope>();

        public ICollection<BookTrope> BookTropes { get; set; } = new List<BookTrope>();
    }
}
