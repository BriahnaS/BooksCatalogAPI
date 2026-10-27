namespace BooksCatalogAPI.Service.Interfaces
{
    public interface IEmbeddingService
    {
        //On hold currently
        //Will revisit when I'm a little further along
        Task<float[]> GenerateEmbeddingAsync(string text);
    }
}
