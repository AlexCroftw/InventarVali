using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNewJointTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutovehiculeInvoice_Autovehicule_AutovehiculeId",
                table: "AutovehiculeInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoicesId",
                table: "AutovehiculeInvoice");

            migrationBuilder.RenameColumn(
                name: "InvoicesId",
                table: "AutovehiculeInvoice",
                newName: "InvoiceFKID");

            migrationBuilder.RenameColumn(
                name: "AutovehiculeId",
                table: "AutovehiculeInvoice",
                newName: "AutovehiculeFKID");

            migrationBuilder.RenameIndex(
                name: "IX_AutovehiculeInvoice_InvoicesId",
                table: "AutovehiculeInvoice",
                newName: "IX_AutovehiculeInvoice_InvoiceFKID");

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
                name: "FK_AutovehiculeInvoice_Autovehicule_AutovehiculeFKID",
                table: "AutovehiculeInvoice",
                column: "AutovehiculeFKID",
                principalTable: "Autovehicule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoiceFKID",
                table: "AutovehiculeInvoice",
                column: "InvoiceFKID",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutovehiculeInvoice_Autovehicule_AutovehiculeFKID",
                table: "AutovehiculeInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_AutovehiculeInvoice_Invoice_InvoiceFKID",
                table: "AutovehiculeInvoice");

            migrationBuilder.DropColumn(
                name: "FuelConsumed",
                table: "AutovehiculeInvoice");

            migrationBuilder.DropColumn(
                name: "PriceFuel",
                table: "AutovehiculeInvoice");

            migrationBuilder.RenameColumn(
                name: "InvoiceFKID",
                table: "AutovehiculeInvoice",
                newName: "InvoicesId");

            migrationBuilder.RenameColumn(
                name: "AutovehiculeFKID",
                table: "AutovehiculeInvoice",
                newName: "AutovehiculeId");

            migrationBuilder.RenameIndex(
                name: "IX_AutovehiculeInvoice_InvoiceFKID",
                table: "AutovehiculeInvoice",
                newName: "IX_AutovehiculeInvoice_InvoicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutovehiculeInvoice_Autovehicule_AutovehiculeId",
                table: "AutovehiculeInvoice",
                column: "AutovehiculeId",
                principalTable: "Autovehicule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
