using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashGrounds.Rate.Migrations
{
    /// <inheritdoc />
    public partial class DbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rates");

            migrationBuilder.RenameTable(
                name: "TrackRates",
                newName: "TrackRates",
                newSchema: "rates");

            migrationBuilder.RenameTable(
                name: "PostRates",
                newName: "PostRates",
                newSchema: "rates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "TrackRates",
                schema: "rates",
                newName: "TrackRates");

            migrationBuilder.RenameTable(
                name: "PostRates",
                schema: "rates",
                newName: "PostRates");
        }
    }
}
