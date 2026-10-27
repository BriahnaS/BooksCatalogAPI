using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksCatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "EditionTypes",
                columns: table => new
                {
                    EditionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionTypes", x => x.EditionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Narrators",
                columns: table => new
                {
                    NarratorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narrators", x => x.NarratorId);
                });

            migrationBuilder.CreateTable(
                name: "PhyscialFormats",
                columns: table => new
                {
                    FormatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhyscialFormats", x => x.FormatId);
                });

            migrationBuilder.CreateTable(
                name: "Subplot",
                columns: table => new
                {
                    SubplotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subplot", x => x.SubplotId);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    TitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "Trope",
                columns: table => new
                {
                    TropeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trope", x => x.TropeId);
                });

            migrationBuilder.CreateTable(
                name: "GenreSubplots",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    SubplotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreSubplots", x => new { x.GenreId, x.SubplotId });
                    table.ForeignKey(
                        name: "FK_GenreSubplots_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreSubplots_Subplot_SubplotId",
                        column: x => x.SubplotId,
                        principalTable: "Subplot",
                        principalColumn: "SubplotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    EditionTypeId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<int>(type: "int", nullable: true),
                    EmbeddingJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_EditionTypes_EditionTypeId",
                        column: x => x.EditionTypeId,
                        principalTable: "EditionTypes",
                        principalColumn: "EditionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreTropes",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    TropeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreTropes", x => new { x.GenreId, x.TropeId });
                    table.ForeignKey(
                        name: "FK_GenreTropes_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreTropes_Trope_TropeId",
                        column: x => x.TropeId,
                        principalTable: "Trope",
                        principalColumn: "TropeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Audiobooks",
                columns: table => new
                {
                    AudiobookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsSingleNarration = table.Column<bool>(type: "bit", nullable: false),
                    IsDuetNarration = table.Column<bool>(type: "bit", nullable: false),
                    IsDualNarration = table.Column<bool>(type: "bit", nullable: false),
                    IsGraphicAudio = table.Column<bool>(type: "bit", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audiobooks", x => x.AudiobookId);
                    table.ForeignKey(
                        name: "FK_Audiobooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookSubplots",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    SubplotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSubplots", x => new { x.BookId, x.SubplotId });
                    table.ForeignKey(
                        name: "FK_BookSubplots_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookSubplots_Subplot_SubplotId",
                        column: x => x.SubplotId,
                        principalTable: "Subplot",
                        principalColumn: "SubplotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTropes",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    TropeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTropes", x => new { x.BookId, x.TropeId });
                    table.ForeignKey(
                        name: "FK_BookTropes_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTropes_Trope_TropeId",
                        column: x => x.TropeId,
                        principalTable: "Trope",
                        principalColumn: "TropeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ebooks",
                columns: table => new
                {
                    EbookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsAvailableOnKU = table.Column<bool>(type: "bit", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ebooks", x => x.EbookId);
                    table.ForeignKey(
                        name: "FK_Ebooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalBooks",
                columns: table => new
                {
                    PhysicalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    FormatId = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalBooks", x => x.PhysicalId);
                    table.ForeignKey(
                        name: "FK_PhysicalBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysicalBooks_PhyscialFormats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "PhyscialFormats",
                        principalColumn: "FormatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookNarrators",
                columns: table => new
                {
                    AudiobookId = table.Column<int>(type: "int", nullable: false),
                    NarratorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookNarrators", x => new { x.AudiobookId, x.NarratorId });
                    table.ForeignKey(
                        name: "FK_BookNarrators_Audiobooks_AudiobookId",
                        column: x => x.AudiobookId,
                        principalTable: "Audiobooks",
                        principalColumn: "AudiobookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookNarrators_Narrators_NarratorId",
                        column: x => x.NarratorId,
                        principalTable: "Narrators",
                        principalColumn: "NarratorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audiobooks_BookId",
                table: "Audiobooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookNarrators_NarratorId",
                table: "BookNarrators",
                column: "NarratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_EditionTypeId",
                table: "Books",
                column: "EditionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_TitleId",
                table: "Books",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookSubplots_SubplotId",
                table: "BookSubplots",
                column: "SubplotId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTropes_TropeId",
                table: "BookTropes",
                column: "TropeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ebooks_BookId",
                table: "Ebooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreSubplots_SubplotId",
                table: "GenreSubplots",
                column: "SubplotId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreTropes_TropeId",
                table: "GenreTropes",
                column: "TropeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalBooks_BookId",
                table: "PhysicalBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalBooks_FormatId",
                table: "PhysicalBooks",
                column: "FormatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookNarrators");

            migrationBuilder.DropTable(
                name: "BookSubplots");

            migrationBuilder.DropTable(
                name: "BookTropes");

            migrationBuilder.DropTable(
                name: "Ebooks");

            migrationBuilder.DropTable(
                name: "GenreSubplots");

            migrationBuilder.DropTable(
                name: "GenreTropes");

            migrationBuilder.DropTable(
                name: "PhysicalBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Audiobooks");

            migrationBuilder.DropTable(
                name: "Narrators");

            migrationBuilder.DropTable(
                name: "Subplot");

            migrationBuilder.DropTable(
                name: "Trope");

            migrationBuilder.DropTable(
                name: "PhyscialFormats");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "EditionTypes");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
