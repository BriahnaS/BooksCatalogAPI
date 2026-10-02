using Microsoft.EntityFrameworkCore;
using BooksCatalogAPI.Models;
using BooksCatalogAPI.Models.JoinTables;
using BooksCatalogAPI.Models.DTOs;

namespace BooksCatalogAPI.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreSubplot> GenreSubplots { get; set; }
        public DbSet<GenreTrope> GenreTropes { get; set; }
        public DbSet<Trope> Tropes { get; set; }
        public DbSet<BookTrope> BookTropes { get; set; }
        public DbSet<Subplot> Subplots { get; set; }
        public DbSet<BookSubplot> BookSubplots { get; set; }
        public DbSet<EditionType> EditionTypes { get; set; }
        public DbSet<Audiobook> Audiobooks { get; set; }
        public DbSet<BookNarrator> BookNarrators { get; set; }
        public DbSet<Narrator> Narrators { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<PhysicalBook> PhysicalBooks { get; set; }
        public DbSet<PhysicalFormat> PhyscialFormats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book).WithMany(b => b.BookAuthors).HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author).WithMany(a => a.BookAuthors).HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<BookNarrator>().HasKey(bn => new { bn.AudiobookId, bn.NarratorId });

            modelBuilder.Entity<BookTrope>().HasKey(bt => new { bt.BookId, bt.TropeId });

            modelBuilder.Entity<BookSubplot>().HasKey(bs => new { bs.BookId, bs.SubplotId });
            modelBuilder.Entity<Subplot>().ToTable("Subplot");

            modelBuilder.Entity<GenreTrope>().HasKey(gt => new { gt.GenreId, gt.TropeId });
            modelBuilder.Entity<Trope>().ToTable("Trope");


            modelBuilder.Entity<GenreSubplot>().HasKey(gs => new { gs.GenreId, gs.SubplotId });

            modelBuilder.Entity<PhysicalBook>().HasKey(pb => pb.PhysicalId);
            modelBuilder.Entity<PhysicalBook>().HasOne(pb => pb.Book).WithMany(b => b.PhysicalBooks).HasForeignKey(pb => pb.BookId);

            modelBuilder.Entity<PhysicalFormat>().HasKey(pf => pf.FormatId);
            modelBuilder.Entity<PhysicalFormat>().HasMany(pf => pf.PhysicalBooks).WithOne(pb => pb.Format).HasForeignKey(pb => pb.FormatId);

            modelBuilder.Entity<Audiobook>().HasKey(a => a.AudiobookId);
            modelBuilder.Entity<Audiobook>().HasOne(a => a.Book).WithMany(b => b.Audiobooks).HasForeignKey(a => a.BookId);

            modelBuilder.Entity<Ebook>().HasKey(e => e.EbookId);
            modelBuilder.Entity<Ebook>().HasOne(e => e.Book).WithMany(e => e.Ebooks).HasForeignKey(e => e.BookId);
        }
    }
}
