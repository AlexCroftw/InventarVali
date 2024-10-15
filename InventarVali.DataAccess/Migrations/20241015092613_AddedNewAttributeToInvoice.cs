using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewAttributeToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Invoice",
                type: "double precision",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalPrice",
                value: 300000.20000000001);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Invoice");
        }
    }
}
