namespace BooksCatalogAPI.Models
{
    public class PhysicalBook
    {
        public int PhysicalId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int FormatId { get; set; }
        public PhysicalFormat Format { get; set; }

        public DateOnly PublicationDate { get; set; }

    }
}
