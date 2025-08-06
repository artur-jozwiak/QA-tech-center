using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OperationDetailsPropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OperationDetails_OperationId",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "OperationDetailsId",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "Pressure",
                table: "OperationDetails",
                newName: "NoOfPasses");

            migrationBuilder.AddColumn<decimal>(
                name: "GunsPitch",
                table: "OperationDetails",
                type: "decimal(3,1)",
                precision: 3,
                scale: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoBlastingBetweenRows",
                table: "OperationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PressureLeft",
                table: "OperationDetails",
                type: "decimal(3,1)",
                precision: 3,
                scale: 1,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PressureRight",
                table: "OperationDetails",
                type: "decimal(3,1)",
                precision: 3,
                scale: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScanningMode",
                table: "OperationDetails",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationDetails_OperationId",
                table: "OperationDetails",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OperationDetails_OperationId",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "GunsPitch",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "NoBlastingBetweenRows",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "PressureLeft",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "PressureRight",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "ScanningMode",
                table: "OperationDetails");

            migrationBuilder.RenameColumn(
                name: "NoOfPasses",
                table: "OperationDetails",
                newName: "Pressure");

            migrationBuilder.AddColumn<int>(
                name: "OperationDetailsId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OperationDetails_OperationId",
                table: "OperationDetails",
                column: "OperationId",
                unique: true);
        }
    }
}
