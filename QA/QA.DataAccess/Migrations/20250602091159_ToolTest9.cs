using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ToolTest9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToolTestId",
                table: "ComparePoints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComparePoints_ToolTestId",
                table: "ComparePoints",
                column: "ToolTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComparePoints_ToolTests_ToolTestId",
                table: "ComparePoints",
                column: "ToolTestId",
                principalTable: "ToolTests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComparePoints_ToolTests_ToolTestId",
                table: "ComparePoints");

            migrationBuilder.DropIndex(
                name: "IX_ComparePoints_ToolTestId",
                table: "ComparePoints");

            migrationBuilder.DropColumn(
                name: "ToolTestId",
                table: "ComparePoints");
        }
    }
}
