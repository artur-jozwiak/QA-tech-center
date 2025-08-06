using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OperationDetailsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Program",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "SandblastingHeight",
                table: "Operations");

            migrationBuilder.AddColumn<int>(
                name: "OperationDetailsId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OperationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Program = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BurrRate = table.Column<int>(type: "int", nullable: true),
                    Pressure = table.Column<int>(type: "int", nullable: true),
                    Feed = table.Column<int>(type: "int", nullable: true),
                    ProcessTray = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SandblastingHeight = table.Column<int>(type: "int", nullable: true),
                    HeadsQty = table.Column<int>(type: "int", nullable: true),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationDetails_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationDetails_OperationId",
                table: "OperationDetails",
                column: "OperationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "OperationDetailsId",
                table: "Operations");

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
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Program",
                table: "Operations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SandblastingHeight",
                table: "Operations",
                type: "int",
                nullable: true);
        }
    }
}
