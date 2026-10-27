using BooksCatalogAPI.Service.Interfaces;
using Azure.AI.OpenAI;
using OpenAI.Embeddings;
using Azure;
using Azure.Identity;
using Microsoft.Extensions.AI;
using System.Runtime.CompilerServices;

namespace BooksCatalogAPI.Service
{
    public class EmbeddingService
    {
        private readonly AzureOpenAIClient _client;

        public EmbeddingService(AzureOpenAIClient client)
        {
            _client = client;
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            EmbeddingClient embeddingClient = _client.GetEmbeddingClient("text-embedding-BAR-model");

            OpenAIEmbedding embedding = await embeddingClient.GenerateEmbeddingAsync(text);

            return embedding.ToFloats().ToArray();
        }
    }
}
