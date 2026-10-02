namespace BooksCatalogAPI.Models
{
    public class PhysicalFormat
    {
        public int FormatId { get; set; }
        public string Name { get; set; }
        public ICollection<PhysicalBook> PhysicalBooks { get; set; }
    }
}
