using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MadeChangesToDbRegardingInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDKV",
                table: "Autovehicule");

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 200m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceDKV",
                table: "Autovehicule",
                type: "double precision",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PriceDKV",
                value: null);

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2,
                column: "PriceDKV",
                value: null);

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 100m);
        }
    }
}
