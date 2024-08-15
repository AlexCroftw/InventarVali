using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedAutovehiculeToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autovehicule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    VinNumber = table.Column<string>(type: "text", nullable: false),
                    InsurenceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HasITP = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autovehicule", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Autovehicule",
                columns: new[] { "Id", "HasITP", "InsurenceDate", "LicensePlate", "Type", "VinNumber" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 8, 15, 8, 33, 32, 660, DateTimeKind.Utc).AddTicks(7220), "B 06 CAR", "Duba", "1XPWDBTX48D766660" },
                    { 2, false, new DateTime(2024, 8, 15, 8, 33, 32, 660, DateTimeKind.Utc).AddTicks(7222), "CL 06 PLM", "Audi R8", "1XPWDBTX48D766660" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autovehicule");
        }
    }
}
