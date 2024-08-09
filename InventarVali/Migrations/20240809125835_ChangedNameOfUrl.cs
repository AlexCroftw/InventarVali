using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameOfUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrle",
                table: "Goods",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Goods",
                newName: "ImageUrle");
        }
    }
}
