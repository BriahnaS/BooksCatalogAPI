using OpenAI.Chat;
using BooksCatalogAPI.Models.DTOs;
using System.Text.Json;
using Azure.AI.OpenAI;

namespace BooksCatalogAPI.Service
{
    public class BookAnalysisService
    {
        private readonly AzureOpenAIClient _client;

        public BookAnalysisService(AzureOpenAIClient client)
        {
            _client = client;
        }

        public async Task<BookAnalysisResponse> AnalyzeBookAsync(string title)
        {
            var chatClient = _client.GetChatClient("book-analysis-chat");

            var prompt = $$""" 
                    Analyze the book "{{title}}".
                    
                    Return ONLY valid JSON.
                    
                    Requirements:
                    - Include at least 3 tropes if known.
                    - Include at least 3 subplots if known.
                    - Do not return empty array unless the book cannot be identified.

                    Example: 

                    {
                        "tropes": [
                            "Found Family", "Enemies-to-Lovers"
                            ],
                        "subplots": [
                            "Political Intrigue", 
                            "Coming of Age"
                            ]
                    }
                    
                    Now analyze "{{title}}".
                    """;

            var response = await chatClient.CompleteChatAsync(prompt);

            var json = response.Value.Content[0].Text;

            Console.WriteLine("RAW AI RESPONSE");
            Console.WriteLine(json);

            
            var result = JsonSerializer.Deserialize<BookAnalysisResponse>(json)!;

            Console.WriteLine($"Tropes: {result?.Tropes.Count}");

            Console.WriteLine($"Subplots: {result?.Subplots.Count}");

            return result;
        }
    }
}
