using BooksCatalogAPI.Data;
using BooksCatalogAPI.Models;
using BooksCatalogAPI.Service;
using BooksCatalogAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json;

namespace BooksCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogDbContext _context;
        private readonly BookSearchService _bookSearchService;
        private readonly EmbeddingService _embeddingService;
        private readonly RecommendationService _recService;
        private readonly BookAnalysisService _bookAnalysisService;

        public CatalogController(CatalogDbContext context, BookSearchService bookSearchService, EmbeddingService embeddingService, RecommendationService recService, BookAnalysisService bookAnalysisService)
        {
            _context = context;
            _bookSearchService = bookSearchService;
            _embeddingService = embeddingService;
            _recService = recService;
            _bookAnalysisService = bookAnalysisService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _context.Books.Include(b => b.Title).Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title.TitleName,
                Authors = b.BookAuthors.Select(ba => ba.Author.AuthorName).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description
            }).ToListAsync();

            return Ok(books);
        }

        [HttpGet("random")]
        public async Task<ActionResult<BookDto>> GetRandomBook()
        {
            var book = await _context.Books.Include(b => b.Title).Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).OrderBy(r => Guid.NewGuid()).Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title.TitleName,
                Authors = b.BookAuthors.Select(ba => ba.Author.AuthorName).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description
            }).FirstOrDefaultAsync();

            return Ok(book);
        }

        [HttpGet("genres")]
        public async Task<ActionResult<List<GenreDto>>> GetGenres()
        {

            var genres = await _context.Genres.ToListAsync();

            var genreDtos = genres.Select(genres => new GenreDto
            {
                GenreId = genres.GenreId,
                Name = genres.Name
            }).ToList();

            return Ok(genreDtos);
        }

        [HttpPost("genres/tropes")]
        public async Task<ActionResult<List<TropeDto>>> GetGenreTropes([FromBody] List<int> genreIds)
        {
            var tropes = await _context.GenreTropes.Where(gt => genreIds.Contains(gt.GenreId)).Select(gt => gt.Trope).Distinct().ToListAsync();

            var tropeDtos = tropes.Select(t => new TropeDto
            {
                TropeId = t.TropeId,
                Name = t.Name
            }).ToList();

            return Ok(tropeDtos);
        }

        [HttpPost("genres/subplots")]
        public async Task<ActionResult<List<SubplotDto>>> GetGenreSubplots([FromBody] List<int> genreIds)
        {
            var subplots = await _context.GenreSubplots.Where(gs => genreIds.Contains(gs.GenreId)).Select(gs => gs.Subplot).Distinct().ToListAsync();

            var subplotDtos = subplots.Select(s => new SubplotDto
            {
                SubplotId = s.SubplotId,
                Name = s.Name
            }).ToList();

            return Ok(subplotDtos);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<BookDto>>> SearchBooks([FromBody] BookSearchRequest request)
        {
            var books = await _bookSearchService.SearchBooksAsync(request.GenreIds, request.Tropes, request.Subplots);

            var dtos = books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title.TitleName,
                Authors = b.BookAuthors.Select(ba => ba.Author.AuthorName).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description
            }).ToList();

            return Ok(dtos);
        }


        [HttpGet("{id}/generate-embedding")]
        public async Task<IActionResult> GenerateEmbedding(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var text = $"""
                Title: {book.Title}
                Description:{book.Description}
                """;

            var embedding = await _embeddingService.GenerateEmbeddingAsync(text);

            book.EmbeddingJson = JsonSerializer.Serialize(embedding);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                book.BookId,
                Embedding = embedding
            });
        }

        [HttpGet("{id}/recommendations")]
        public async Task<IActionResult> GetRecommendations(int id)
        {
            var recommendations = await _recService.GetRecommendations(id);

            return Ok(recommendations.Select(r => new BookDto
            {
                BookId = r.Book.BookId,
                Title = r.Book.Title.TitleName,
                Authors = r.Book.BookAuthors.Select(a=> a.Author.AuthorName).ToList(),
                CoverImageUrl = r.Book.CoverImageUrl,
                Score = r.Score
            }));
        }

        [HttpGet("test-analysis/{title}")]
        public async Task<IActionResult> TestAnalysis(string title)
        {
            var result = await _bookAnalysisService.AnalyzeBookAsync(title);

            return Ok(result);
        }
    }
}
