using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAutovehiculeToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Autovehicule",
                columns: new[] { "Id", "HasITP", "InsurenceDate", "LicensePlate", "Type", "VinNumber" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), "B 06 CAR", "Duba", "1XPWDBTX48D766660" },
                    { 2, false, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), "CL 06 PLM", "Audi R8", "1XPWDBTX48D766660" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
