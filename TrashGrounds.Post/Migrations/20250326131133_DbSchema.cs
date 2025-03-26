using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashGrounds.Post.Migrations
{
    /// <inheritdoc />
    public partial class DbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Posts",
                newSchema: "posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Posts",
                schema: "posts",
                newName: "Posts");
        }
    }
}
