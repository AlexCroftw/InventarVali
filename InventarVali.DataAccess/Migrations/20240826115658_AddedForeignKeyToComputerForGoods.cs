using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyToComputerForGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodsId",
                table: "Computers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 1,
                column: "GoodsId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 2,
                column: "GoodsId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_GoodsId",
                table: "Computers",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Goods_GoodsId",
                table: "Computers",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Goods_GoodsId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_GoodsId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "GoodsId",
                table: "Computers");
        }
    }
}
