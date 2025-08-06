using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Sintering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SampleLocalization",
                table: "MeasurementsSeries",
                newName: "StorageLocation");

            migrationBuilder.AddColumn<decimal>(
                name: "SpacerHeight",
                table: "Products",
                type: "decimal(3,0)",
                precision: 3,
                scale: 0,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitsPerSinteringTray",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Products",
                type: "decimal(5,3)",
                precision: 5,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Qty",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TraysPerSintering",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrayLocationId",
                table: "MeasurementsSeries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FuranceLocalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StackNo = table.Column<int>(type: "int", nullable: false),
                    LevelNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuranceLocalizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sinterings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinterings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrayLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StackNo = table.Column<int>(type: "int", nullable: false),
                    LevelNo = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrayCoating = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    RowDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    FuranceLocalizationId = table.Column<int>(type: "int", nullable: false),
                    SinteringId = table.Column<int>(type: "int", nullable: true),
                    IsScrapTray = table.Column<bool>(type: "bit", nullable: false),
                    IsEmptyTray = table.Column<bool>(type: "bit", nullable: false),
                    ContainsMasterSample = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrayLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrayLocations_FuranceLocalizations_FuranceLocalizationId",
                        column: x => x.FuranceLocalizationId,
                        principalTable: "FuranceLocalizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrayLocations_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrayLocations_Sinterings_SinteringId",
                        column: x => x.SinteringId,
                        principalTable: "Sinterings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsSeries_TrayLocationId",
                table: "MeasurementsSeries",
                column: "TrayLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrayLocations_FuranceLocalizationId",
                table: "TrayLocations",
                column: "FuranceLocalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrayLocations_OrderId",
                table: "TrayLocations",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TrayLocations_SinteringId",
                table: "TrayLocations",
                column: "SinteringId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementsSeries_TrayLocations_TrayLocationId",
                table: "MeasurementsSeries",
                column: "TrayLocationId",
                principalTable: "TrayLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementsSeries_TrayLocations_TrayLocationId",
                table: "MeasurementsSeries");

            migrationBuilder.DropTable(
                name: "TrayLocations");

            migrationBuilder.DropTable(
                name: "FuranceLocalizations");

            migrationBuilder.DropTable(
                name: "Sinterings");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementsSeries_TrayLocationId",
                table: "MeasurementsSeries");

            migrationBuilder.DropColumn(
                name: "SpacerHeight",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitsPerSinteringTray",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TraysPerSintering",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TrayLocationId",
                table: "MeasurementsSeries");

            migrationBuilder.RenameColumn(
                name: "StorageLocation",
                table: "MeasurementsSeries",
                newName: "SampleLocalization");
        }
    }
}
