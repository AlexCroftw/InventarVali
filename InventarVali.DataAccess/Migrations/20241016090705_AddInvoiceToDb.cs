using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Autovehicule_AutovehiculeFKId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_AutovehiculeFKId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "AutovehiculeFKId",
                table: "Invoice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutovehiculeFKId",
                table: "Invoice",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 1,
                column: "AutovehiculeFKId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_AutovehiculeFKId",
                table: "Invoice",
                column: "AutovehiculeFKId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Autovehicule_AutovehiculeFKId",
                table: "Invoice",
                column: "AutovehiculeFKId",
                principalTable: "Autovehicule",
                principalColumn: "Id");
        }
    }
}
