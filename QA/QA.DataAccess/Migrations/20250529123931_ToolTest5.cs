using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ToolTest5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparePoints_ToolTestComparisons_ToolTestComparisonId",
                table: "ComparePoints");

            migrationBuilder.AlterColumn<int>(
                name: "ToolTestComparisonId",
                table: "ComparePoints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparePoints_ToolTestComparisons_ToolTestComparisonId",
                table: "ComparePoints",
                column: "ToolTestComparisonId",
                principalTable: "ToolTestComparisons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparePoints_ToolTestComparisons_ToolTestComparisonId",
                table: "ComparePoints");

            migrationBuilder.AlterColumn<int>(
                name: "ToolTestComparisonId",
                table: "ComparePoints",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComparePoints_ToolTestComparisons_ToolTestComparisonId",
                table: "ComparePoints",
                column: "ToolTestComparisonId",
                principalTable: "ToolTestComparisons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
