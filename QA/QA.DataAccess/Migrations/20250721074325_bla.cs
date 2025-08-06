using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ComparePoints_ComparisonPointId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ComparePoints_ComparisonPointId",
                table: "Images",
                column: "ComparisonPointId",
                principalTable: "ComparePoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ComparePoints_ComparisonPointId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ComparePoints_ComparisonPointId",
                table: "Images",
                column: "ComparisonPointId",
                principalTable: "ComparePoints",
                principalColumn: "Id");
        }
    }
}
