using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrashGrounds.Track.Migrations
{
    /// <inheritdoc />
    public partial class DbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("251ceebe-752a-4ea4-810e-03b8eaee7e5f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("50e3aa21-ada0-42d3-a143-cf45922b97a5"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d4b7bc34-3b01-4247-a9bc-02d4c5185e08"));

            migrationBuilder.EnsureSchema(
                name: "tracks");

            migrationBuilder.RenameTable(
                name: "MusicTracks",
                newName: "MusicTracks",
                newSchema: "tracks");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genres",
                newSchema: "tracks");

            migrationBuilder.RenameTable(
                name: "GenreMusicTrack",
                newName: "GenreMusicTrack",
                newSchema: "tracks");

            migrationBuilder.InsertData(
                schema: "tracks",
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("21395468-d9de-4954-a1d9-8f8d4111f024"), "Русский рэп" },
                    { new Guid("26f5682b-d20d-43e0-81fc-3c0a4bdc4fc3"), "Что-то странное" },
                    { new Guid("c24b1a5a-431b-40a8-a3f9-b79573dd4e4f"), "Мэшап" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "tracks",
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("21395468-d9de-4954-a1d9-8f8d4111f024"));

            migrationBuilder.DeleteData(
                schema: "tracks",
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("26f5682b-d20d-43e0-81fc-3c0a4bdc4fc3"));

            migrationBuilder.DeleteData(
                schema: "tracks",
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c24b1a5a-431b-40a8-a3f9-b79573dd4e4f"));

            migrationBuilder.RenameTable(
                name: "MusicTracks",
                schema: "tracks",
                newName: "MusicTracks");

            migrationBuilder.RenameTable(
                name: "Genres",
                schema: "tracks",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "GenreMusicTrack",
                schema: "tracks",
                newName: "GenreMusicTrack");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("251ceebe-752a-4ea4-810e-03b8eaee7e5f"), "Что-то странное" },
                    { new Guid("50e3aa21-ada0-42d3-a143-cf45922b97a5"), "Мэшап" },
                    { new Guid("d4b7bc34-3b01-4247-a9bc-02d4c5185e08"), "Русский рэп" }
                });
        }
    }
}
