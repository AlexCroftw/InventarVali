using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyToEmployeeForGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodsId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "GoodsId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "GoodsId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "GoodsId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GoodsId",
                table: "Employees",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Goods_GoodsId",
                table: "Employees",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Goods_GoodsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GoodsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GoodsId",
                table: "Employees");
        }
    }
}
