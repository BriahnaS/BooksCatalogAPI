using BooksCatalogAPI.Data;
using BooksCatalogAPI.Models;
using BooksCatalogAPI.Service;
using BooksCatalogAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogDbContext _context;
        private readonly BookSearchService _bookSearchService;

        public CatalogController(CatalogDbContext context, BookSearchService bookSearchService)
        {
            _context = context;
            _bookSearchService = bookSearchService;
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
    }
}
