using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedGoodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "ImageUrl", "IsTaken", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "", true, "Masina", "Dacia Duster" },
                    { 2, "", false, "Masina", "Audi A6" },
                    { 3, "", true, "Laptop", "Asus Rog" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Goods",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
