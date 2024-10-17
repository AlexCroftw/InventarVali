using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedInvoiceToDbPlusFKToAutovehicule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    CardNumber = table.Column<string>(type: "text", nullable: false),
                    AutovehiculeFKId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Autovehicule_AutovehiculeFKId",
                        column: x => x.AutovehiculeFKId,
                        principalTable: "Autovehicule",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "AutovehiculeFKId", "CardNumber", "InvoiceDate", "InvoiceNumber", "Price" },
                values: new object[] { 1, 2, "704310.0109124771", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "24/000838503/997", 2000.1199999999999 });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_AutovehiculeFKId",
                table: "Invoice",
                column: "AutovehiculeFKId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
