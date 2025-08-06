using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Tool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestType",
                table: "ToolTests");

            migrationBuilder.AddColumn<int>(
                name: "TestType",
                table: "ToolTestComparisons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToolId",
                table: "ToolTestComparisons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tool", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToolTestComparisons_ToolId",
                table: "ToolTestComparisons",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToolTestComparisons_Tool_ToolId",
                table: "ToolTestComparisons",
                column: "ToolId",
                principalTable: "Tool",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToolTestComparisons_Tool_ToolId",
                table: "ToolTestComparisons");

            migrationBuilder.DropTable(
                name: "Tool");

            migrationBuilder.DropIndex(
                name: "IX_ToolTestComparisons_ToolId",
                table: "ToolTestComparisons");

            migrationBuilder.DropColumn(
                name: "TestType",
                table: "ToolTestComparisons");

            migrationBuilder.DropColumn(
                name: "ToolId",
                table: "ToolTestComparisons");

            migrationBuilder.AddColumn<int>(
                name: "TestType",
                table: "ToolTests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
