namespace BooksCatalogAPI.Models.JoinTables
{
    public class BookSubplot
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int SubplotId { get; set; }
        public Subplot Subplot { get; set; }
    }
}
