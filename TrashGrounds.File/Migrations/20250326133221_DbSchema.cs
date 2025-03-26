using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashGrounds.File.Migrations
{
    /// <inheritdoc />
    public partial class DbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "files");

            migrationBuilder.RenameTable(
                name: "MusicFiles",
                newName: "MusicFiles",
                newSchema: "files");

            migrationBuilder.RenameTable(
                name: "ImageFiles",
                newName: "ImageFiles",
                newSchema: "files");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "MusicFiles",
                schema: "files",
                newName: "MusicFiles");

            migrationBuilder.RenameTable(
                name: "ImageFiles",
                schema: "files",
                newName: "ImageFiles");
        }
    }
}
