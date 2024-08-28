using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFkToComputerUsingEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Computers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Computers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmployeeId", "ImageUrl" },
                values: new object[] { 2, "" });

            migrationBuilder.UpdateData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmployeeId", "ImageUrl" },
                values: new object[] { 1, "" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "John Doe");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Michael Cox");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Vasile Braconieru");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_EmployeeId",
                table: "Computers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Employees_EmployeeId",
                table: "Computers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Employees_EmployeeId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_EmployeeId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Computers");
        }
    }
}
