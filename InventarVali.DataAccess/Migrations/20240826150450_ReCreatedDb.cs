using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReCreatedDb : Migration
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
                    InsurenceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HasITP = table.Column<bool>(type: "boolean", nullable: false),
                    ITPExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InsuranceExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HasVinieta = table.Column<bool>(type: "boolean", nullable: false),
                    VinietaExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autovehicule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    IsTaken = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Autovehicule",
                columns: new[] { "Id", "HasITP", "HasVinieta", "ITPExpirationDate", "InsuranceExpirationDate", "InsurenceDate", "LicensePlate", "Type", "VinNumber", "VinietaExpirationDate" },
                values: new object[,]
                {
                    { 1, true, true, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), "B 06 CAR", "Duba", "1XPWDBTX48D766660", new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, false, false, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), "CL 06 PLM", "Audi R8", "1XPWDBTX48D766660", new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Computers",
                columns: new[] { "Id", "Description", "Model", "SerialNumber", "Type" },
                values: new object[,]
                {
                    { 1, "Intel Core I9, RTX 4070, 32 GB RAM", "Asus Rog", "12-12AB3", "Laptop" },
                    { 2, "Intel Core I5, no GPU, 32 GB RAM", "x570 Aorus Elite", null, "Desktop" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "test@email.com", "John", "Doe" },
                    { 2, "test2@email.com", "Michael", "Cox" },
                    { 3, "test4@email.com", "Vasile", "Braconieru" }
                });

            migrationBuilder.InsertData(
                table: "Goods",
                columns: new[] { "Id", "ImageUrl", "IsTaken", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "", true, "Masina", "Dacia Duster" },
                    { 2, "", false, "Masina", "Audi A6" },
                    { 3, "", true, "Laptop", "Asus Rog" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autovehicule");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Goods");
        }
    }
}
