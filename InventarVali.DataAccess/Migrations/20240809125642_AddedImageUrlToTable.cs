using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrlToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrle",
                table: "Goods",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrle",
                table: "Goods");
        }
    }
}
