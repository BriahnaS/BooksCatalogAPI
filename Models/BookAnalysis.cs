namespace BooksCatalogAPI.Models
{
    public class BookAnalysis
    {
        public List<string> Tropes { get; set; } = [];
        public List<string> Themes { get; set; } = [];
        public List<string> Subplots { get; set; } = [];
    }
}
