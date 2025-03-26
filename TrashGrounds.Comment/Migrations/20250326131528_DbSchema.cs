using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashGrounds.Comment.Migrations
{
    /// <inheritdoc />
    public partial class DbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comments",
                newSchema: "comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Comments",
                schema: "comments",
                newName: "Comments");
        }
    }
}
