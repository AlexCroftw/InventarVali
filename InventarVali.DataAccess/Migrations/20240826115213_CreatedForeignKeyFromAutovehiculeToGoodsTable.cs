using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedForeignKeyFromAutovehiculeToGoodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodsId",
                table: "Autovehicule",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1,
                column: "GoodsId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2,
                column: "GoodsId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Autovehicule_GoodsId",
                table: "Autovehicule",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autovehicule_Goods_GoodsId",
                table: "Autovehicule",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autovehicule_Goods_GoodsId",
                table: "Autovehicule");

            migrationBuilder.DropIndex(
                name: "IX_Autovehicule_GoodsId",
                table: "Autovehicule");

            migrationBuilder.DropColumn(
                name: "GoodsId",
                table: "Autovehicule");
        }
    }
}
