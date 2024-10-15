using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedInvoiceUrlToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceUrl",
                table: "Invoice",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "InvoiceUrl",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceUrl",
                table: "Invoice");
        }
    }
}
