using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrashGrounds.User.Migrations
{
    /// <inheritdoc />
    public partial class DbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("59174f05-e8b2-444c-8e4e-bfc13e25aba6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6fe9e721-4910-4b2c-b626-23949805fa61"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c5d94634-95d8-424c-98b1-214dbeafbade"));

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.RenameTable(
                name: "DomainUsers",
                newName: "DomainUsers",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "users");

            migrationBuilder.InsertData(
                schema: "users",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("941f7059-873f-40e0-b7b7-871bf0297a88"), null, "Admin", "ADMIN" },
                    { new Guid("a1942a56-edb8-400c-a8b2-b6e114564b06"), null, "Moderator", "MODERATOR" },
                    { new Guid("e73aafb0-5637-4e5c-8497-ec20791dcd93"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("941f7059-873f-40e0-b7b7-871bf0297a88"));

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1942a56-edb8-400c-a8b2-b6e114564b06"));

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e73aafb0-5637-4e5c-8497-ec20791dcd93"));

            migrationBuilder.RenameTable(
                name: "DomainUsers",
                schema: "users",
                newName: "DomainUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "users",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "users",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "users",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "users",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "users",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "users",
                newName: "AspNetRoleClaims");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("59174f05-e8b2-444c-8e4e-bfc13e25aba6"), null, "Admin", "ADMIN" },
                    { new Guid("6fe9e721-4910-4b2c-b626-23949805fa61"), null, "User", "USER" },
                    { new Guid("c5d94634-95d8-424c-98b1-214dbeafbade"), null, "Moderator", "MODERATOR" }
                });
        }
    }
}
