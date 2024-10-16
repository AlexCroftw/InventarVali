using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateJointTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutovehiculeInvoice",
                columns: table => new
                {
                    AutovehiculeId = table.Column<int>(type: "integer", nullable: false),
                    InvoicesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutovehiculeInvoice", x => new { x.AutovehiculeId, x.InvoicesId });
                    table.ForeignKey(
                        name: "FK_AutovehiculeInvoice_Autovehicule_AutovehiculeId",
                        column: x => x.AutovehiculeId,
                        principalTable: "Autovehicule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutovehiculeInvoice_Invoice_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutovehiculeInvoice_InvoicesId",
                table: "AutovehiculeInvoice",
                column: "InvoicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutovehiculeInvoice");
        }
    }
}
