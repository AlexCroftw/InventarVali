using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPricePropertyInInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Invoice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Invoice",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 200m);
        }
    }
}
