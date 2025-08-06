using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SandblastingConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EdgeNo",
                table: "Parameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BurrRate",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Feed",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadsQty",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pressure",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessTray",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SandblastingHeight",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EdgeNo",
                table: "Measurements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementSeriesId",
                table: "Measurements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeasurementsSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    TrayNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PositionOnTray = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementsSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementsSeries_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementsSeries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasurementSeriesId",
                table: "Measurements",
                column: "MeasurementSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsSeries_OperationId",
                table: "MeasurementsSeries",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsSeries_OrderId",
                table: "MeasurementsSeries",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasurementsSeries_MeasurementSeriesId",
                table: "Measurements",
                column: "MeasurementSeriesId",
                principalTable: "MeasurementsSeries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasurementsSeries_MeasurementSeriesId",
                table: "Measurements");

            migrationBuilder.DropTable(
                name: "MeasurementsSeries");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_MeasurementSeriesId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "EdgeNo",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "BurrRate",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Feed",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "HeadsQty",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ProcessTray",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "SandblastingHeight",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "EdgeNo",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasurementSeriesId",
                table: "Measurements");
        }
    }
}
