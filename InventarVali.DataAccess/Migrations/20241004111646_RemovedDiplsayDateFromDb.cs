using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarVali.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDiplsayDateFromDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayITPExpirationDate",
                table: "Autovehicule");

            migrationBuilder.DropColumn(
                name: "DisplayInsurenceExpirationDate",
                table: "Autovehicule");

            migrationBuilder.DropColumn(
                name: "DisplayVinietaExpirationDate",
                table: "Autovehicule");

            migrationBuilder.DropColumn(
                name: "InsurenceDisplayDate",
                table: "Autovehicule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DisplayITPExpirationDate",
                table: "Autovehicule",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DisplayInsurenceExpirationDate",
                table: "Autovehicule",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DisplayVinietaExpirationDate",
                table: "Autovehicule",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsurenceDisplayDate",
                table: "Autovehicule",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayITPExpirationDate", "DisplayInsurenceExpirationDate", "DisplayVinietaExpirationDate", "InsurenceDisplayDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Autovehicule",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DisplayITPExpirationDate", "DisplayInsurenceExpirationDate", "DisplayVinietaExpirationDate", "InsurenceDisplayDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
