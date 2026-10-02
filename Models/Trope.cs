using BooksCatalogAPI.Models.JoinTables;
namespace BooksCatalogAPI.Models
{
    public class Trope
    {
        public int TropeId { get; set; }
        public string Name { get; set; }
        public ICollection<GenreTrope> GenreTropes { get; set; }
    }
}
