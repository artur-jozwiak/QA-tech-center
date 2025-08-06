using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertiesInMeasurementSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrainSize",
                table: "MeasurementsSeries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PorosityClass",
                table: "MeasurementsSeries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SampleLocalization",
                table: "MeasurementsSeries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StackNo",
                table: "MeasurementsSeries",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrainSize",
                table: "MeasurementsSeries");

            migrationBuilder.DropColumn(
                name: "PorosityClass",
                table: "MeasurementsSeries");

            migrationBuilder.DropColumn(
                name: "SampleLocalization",
                table: "MeasurementsSeries");

            migrationBuilder.DropColumn(
                name: "StackNo",
                table: "MeasurementsSeries");
        }
    }
}
