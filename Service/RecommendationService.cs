using BooksCatalogAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using BooksCatalogAPI.Models;

namespace BooksCatalogAPI.Service
{
    public class RecommendationService
    {
        private readonly CatalogDbContext _context;

        public RecommendationService(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<double> CompareBooks(int bookId1, int bookId2)
        {
            var book1 = await _context.Books.FindAsync(bookId1);

            var book2 = await _context.Books.FindAsync(bookId2);

            if (book1 == null || book2 == null)
                throw new Exception("Book not found");

            if (string.IsNullOrWhiteSpace(book1.EmbeddingJson) || string.IsNullOrWhiteSpace(book2.EmbeddingJson))
            {
                throw new Exception("One of both books do not have embeddings.");
            }

            var embedding1 = JsonSerializer.Deserialize<float[]>(book1.EmbeddingJson);

            var embedding2 = JsonSerializer.Deserialize<float[]>(book2.EmbeddingJson);

            if (embedding1 == null || embedding2 == null)
            {
                throw new Exception("Failed to deserialize embedding.");
            }

            return CosineSimilarity(embedding1!, embedding2!);
        }

        public static double CosineSimilarity(float[] a, float[] b)
        {
            double dotProduct = 0;
            double magnitudeA = 0;
            double magnitudeB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                dotProduct += a[i] * b[i];
                magnitudeA += a[i] * a[i];
                magnitudeB += b[i] * b[i];
            }

            return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }

        public async Task<List<(Book Book, double Score)>> GetRecommendations(int bookId)
        {
            var targetBook = await _context.Books.FindAsync(bookId);

            var targetEmbedding = JsonSerializer.Deserialize<float[]>(targetBook!.EmbeddingJson!)!;

            var books = await _context.Books.ToListAsync();

            var recommendations = books.Where(b => b.BookId != bookId).Select(book =>
            {
                var embedding = JsonSerializer.Deserialize<float[]>(book.EmbeddingJson!)!;

                return new
                {
                    Book = book,
                    Score = CosineSimilarity(targetEmbedding, embedding)
                };
            }).OrderByDescending(x => x.Score).Take(10);

            return recommendations.Select(x => (x.Book, x.Score)).ToList();
        }
    }
}
