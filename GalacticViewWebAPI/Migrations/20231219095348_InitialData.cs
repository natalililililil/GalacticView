using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GalacticViewWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90a28f65-62ab-40c2-8924-6340c9d73b8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5c58ad7-1a54-4ce2-a6dd-c2b5e93c01bf");

            migrationBuilder.InsertData(
                table: "AllNews",
                columns: new[] { "NewsId", "Subtitle", "Text", "Title", "TitleImagePath", "URL" },
                values: new object[] { new Guid("93f37ae5-51d4-412a-871f-47780237aa4f"), "это подзаголовок", "Software developer ваыраыоаыи ыраггыак ико кррк уоарк к а", "Новость1", "путь1", "/news" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37c81399-8a5f-40c0-9fc0-5bd7448c7fa2", "8f7ec1e5-49ba-4eb1-bcad-615ec6451b14", "Manager", "MANAGER" },
                    { "d4496d1f-6947-47a7-a12c-98a3485212bd", "55f4a491-5b1c-46ad-a4d0-a1ea144353c3", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AllNews",
                keyColumn: "NewsId",
                keyValue: new Guid("93f37ae5-51d4-412a-871f-47780237aa4f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37c81399-8a5f-40c0-9fc0-5bd7448c7fa2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4496d1f-6947-47a7-a12c-98a3485212bd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "90a28f65-62ab-40c2-8924-6340c9d73b8b", "fb10e1c4-8568-49f4-9ee7-affeebb3c516", "Manager", "MANAGER" },
                    { "a5c58ad7-1a54-4ce2-a6dd-c2b5e93c01bf", "581572e2-2fea-4984-8fe9-26cb4164ade3", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
