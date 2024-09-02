using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedGoodsFromDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    IsTaken = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
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
    }
}
