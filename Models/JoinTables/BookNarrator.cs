namespace BooksCatalogAPI.Models.JoinTables
{
    public class BookNarrator
    {
        public int AudiobookId { get; set; }
        public Audiobook Audiobook { get; set; }

        public int NarratorId { get; set; }
        public Narrator Narrator { get; set; }
    }
}
