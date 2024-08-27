using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFKForEmployeeIdToAutovehicule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Autovehicule",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Autovehicule",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmployeeId", "ImageUrl" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmployeeId", "ImageUrl" },
                values: new object[] { 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_Autovehicule_EmployeeId",
                table: "Autovehicule",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autovehicule_Employees_EmployeeId",
                table: "Autovehicule",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autovehicule_Employees_EmployeeId",
                table: "Autovehicule");

            migrationBuilder.DropIndex(
                name: "IX_Autovehicule_EmployeeId",
                table: "Autovehicule");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Autovehicule");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Autovehicule");
        }
    }
}
