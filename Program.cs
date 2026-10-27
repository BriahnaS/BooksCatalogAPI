using Microsoft.EntityFrameworkCore;
using BooksCatalogAPI.Models;
using BooksCatalogAPI.Data;
using BooksCatalogAPI.Service;
using Azure;
using Azure.AI.OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CatalogDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<BookSearchService>();

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    return new AzureOpenAIClient(new Uri(configuration["AZURE_OPENAI_ENDPOINT"]), new AzureKeyCredential(configuration["AZURE_OPENAI_API_KEY"]));
});

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddScoped<EmbeddingService>();
builder.Services.AddScoped<RecommendationService>();
builder.Services.AddScoped<BookAnalysisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
