using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNewJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoicesId",
                table: "AutovehiculeInvoice");

            migrationBuilder.RenameColumn(
                name: "InvoicesId",
                table: "AutovehiculeInvoice",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_AutovehiculeInvoice_InvoicesId",
                table: "AutovehiculeInvoice",
                newName: "IX_AutovehiculeInvoice_InvoiceId");

            migrationBuilder.AddColumn<double>(
                name: "FuelConsumed",
                table: "AutovehiculeInvoice",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceFuel",
                table: "AutovehiculeInvoice",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoiceId",
                table: "AutovehiculeInvoice",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoiceId",
                table: "AutovehiculeInvoice");

            migrationBuilder.DropColumn(
                name: "FuelConsumed",
                table: "AutovehiculeInvoice");

            migrationBuilder.DropColumn(
                name: "PriceFuel",
                table: "AutovehiculeInvoice");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "AutovehiculeInvoice",
                newName: "InvoicesId");

            migrationBuilder.RenameIndex(
                name: "IX_AutovehiculeInvoice_InvoiceId",
                table: "AutovehiculeInvoice",
                newName: "IX_AutovehiculeInvoice_InvoicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoicesId",
                table: "AutovehiculeInvoice",
                column: "InvoicesId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
