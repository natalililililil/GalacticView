using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GalacticViewWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78b13e1e-28cd-461d-98a8-473f75514579", "a56e597b-a715-476a-a902-9a8d2d8d833a", "Manager", "MANAGER" },
                    { "8ad908d1-8740-4703-9179-121fac4c2f9a", "620ff153-82d3-4cf5-97ce-e185e1b55ebb", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78b13e1e-28cd-461d-98a8-473f75514579");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ad908d1-8740-4703-9179-121fac4c2f9a");
        }
    }
}
