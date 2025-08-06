using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ToolTest3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComparisonParameter",
                table: "ToolTestComparisons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ComparisonPointId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComparePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToolTestComparisonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparePoints_ToolTestComparisons_ToolTestComparisonId",
                        column: x => x.ToolTestComparisonId,
                        principalTable: "ToolTestComparisons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ComparisonPointId",
                table: "Images",
                column: "ComparisonPointId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparePoints_ToolTestComparisonId",
                table: "ComparePoints",
                column: "ToolTestComparisonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ComparePoints_ComparisonPointId",
                table: "Images",
                column: "ComparisonPointId",
                principalTable: "ComparePoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ComparePoints_ComparisonPointId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ComparePoints");

            migrationBuilder.DropIndex(
                name: "IX_Images_ComparisonPointId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ComparisonParameter",
                table: "ToolTestComparisons");

            migrationBuilder.DropColumn(
                name: "ComparisonPointId",
                table: "Images");
        }
    }
}
