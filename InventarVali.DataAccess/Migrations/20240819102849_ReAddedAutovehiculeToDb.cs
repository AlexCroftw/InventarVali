using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReAddedAutovehiculeToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsurenceDate",
                value: new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsurenceDate",
                value: new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsurenceDate",
                value: new DateTime(2024, 8, 15, 8, 33, 32, 660, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsurenceDate",
                value: new DateTime(2024, 8, 15, 8, 33, 32, 660, DateTimeKind.Utc).AddTicks(7222));
        }
    }
}
