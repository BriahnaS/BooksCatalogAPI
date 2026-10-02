namespace BooksCatalogAPI.Models.JoinTables
{
    public class GenreTrope
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int TropeId { get; set; }
        public Trope Trope { get; set; }
    }
}
