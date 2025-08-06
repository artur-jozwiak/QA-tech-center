using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ToolTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToolTestComparisons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestAim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TypeOfMachinning = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorpieceDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorpieceHardness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Machine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolTestComparisons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolderType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ProductSymbol = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Application = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Substrate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Coating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CoatingThickness = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    VisualInspection = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Other = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Feedf = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FeedVf = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SpeedVc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CuttingDepth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NumberOfPasses = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Time = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Cooling = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ChipShape = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WorkpieceRoughness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisualDamageDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EdgeWear = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Chipping = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PlasticDeformation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ComparisonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolTests_ToolTestComparisons_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "ToolTestComparisons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToolTests_ComparisonId",
                table: "ToolTests",
                column: "ComparisonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToolTests");

            migrationBuilder.DropTable(
                name: "ToolTestComparisons");
        }
    }
}
