namespace BooksCatalogAPI.Models.JoinTables
{
    public class GenreSubplot
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int SubplotId { get; set; }
        public Subplot Subplot { get; set; }
    }
}
