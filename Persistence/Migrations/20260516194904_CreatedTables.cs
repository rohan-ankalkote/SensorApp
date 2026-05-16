using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Threshold = table.Column<int>(type: "INTEGER", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Interval = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Flag = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReadingTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceReadings_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThresholdAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceReadingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Flag = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThresholdAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThresholdAlerts_DeviceReadings_DeviceReadingId",
                        column: x => x.DeviceReadingId,
                        principalTable: "DeviceReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DeviceId",
                table: "AuditLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReadings_DeviceId",
                table: "DeviceReadings",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ThresholdAlerts_DeviceReadingId",
                table: "ThresholdAlerts",
                column: "DeviceReadingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ThresholdAlerts");

            migrationBuilder.DropTable(
                name: "DeviceReadings");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
