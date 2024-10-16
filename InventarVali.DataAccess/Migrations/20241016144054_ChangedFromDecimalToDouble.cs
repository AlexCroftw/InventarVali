using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFromDecimalToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Invoice",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 200.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Invoice",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 200m);
        }
    }
}
