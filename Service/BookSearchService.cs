using Azure.Core;
using BooksCatalogAPI.Models;
using BooksCatalogAPI.Models.DTOs;
using BooksCatalogAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksCatalogAPI.Service
{
    public class BookSearchService
    {
        private readonly CatalogDbContext _context;

        public BookSearchService(CatalogDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> SearchBooksAsync(List<int>? genreIds, List<int>? tropeIds, List<int>? subplotIds)
        {
            var query = _context.Books
                .Include(b => b.Title)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookTropes)
                .Include(b => b.BookSubplots)
                .AsQueryable();


            if (genreIds != null && genreIds.Any())
            {
                query = query.Where(b => genreIds.Contains(b.GenreId));
            }

            if (tropeIds != null && tropeIds.Any())
            {
                query = query.Where(b => b.BookTropes.Any(bt => tropeIds.Contains(bt.TropeId)));
            }

            if (subplotIds != null && subplotIds.Any())
            {
                query = query.Where(b => b.BookSubplots.Any(bs => subplotIds.Contains(bs.SubplotId)));
            }

            return await query.ToListAsync();
        }
    }
}
