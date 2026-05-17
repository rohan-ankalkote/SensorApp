using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "DeviceReadings",
                newName: "TertiaryValue");

            migrationBuilder.AddColumn<double>(
                name: "PrimaryValue",
                table: "DeviceReadings",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SecondaryValue",
                table: "DeviceReadings",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryValue",
                table: "DeviceReadings");

            migrationBuilder.DropColumn(
                name: "SecondaryValue",
                table: "DeviceReadings");

            migrationBuilder.RenameColumn(
                name: "TertiaryValue",
                table: "DeviceReadings",
                newName: "Value");
        }
    }
}
