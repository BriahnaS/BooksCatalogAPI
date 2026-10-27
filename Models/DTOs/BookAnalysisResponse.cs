using System.Text.Json.Serialization;

namespace BooksCatalogAPI.Models.DTOs
{
    public class BookAnalysisResponse
    {
        [JsonPropertyName("subplots")]
        public List<string> Subplots { get; set; } = new();

        [JsonPropertyName("tropes")]
        public List<string> Tropes { get; set; } = new(); 
    }
}
