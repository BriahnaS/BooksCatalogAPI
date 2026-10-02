namespace BooksCatalogAPI.Models.JoinTables
{
    public class BookTrope
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int TropeId { get; set; }
        public Trope Trope { get; set; }
    }
}
