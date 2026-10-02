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
            // No-op: this migration was generated to match an existing schema.
            // Applying it will mark the migration as applied without attempting to recreate objects that already exist.
            migrationBuilder.Sql("/* InitialCreate migration intentionally skipped because schema exists in target database */");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op reverse. If you need to remove the table, do so manually in the database or create a tailored migration.
            migrationBuilder.Sql("/* InitialCreate Down intentionally left blank */");
        }
    }
}
