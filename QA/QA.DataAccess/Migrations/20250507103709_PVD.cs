using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PVD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoatingProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunNo = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Coating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoatingProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoatingSymbol = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CoatingName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    InternalName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LSL = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    USL = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoatingMeasurementSeriess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    TowerNo = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    NonRotatingRod = table.Column<bool>(type: "bit", nullable: false),
                    IsReferenceSample = table.Column<bool>(type: "bit", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thickness1 = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Thickness2 = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Thickness3 = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Thickness4 = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Adhesion1 = table.Column<int>(type: "int", nullable: true),
                    Adhesion2 = table.Column<int>(type: "int", nullable: true),
                    Adhesion3 = table.Column<int>(type: "int", nullable: true),
                    Adhesion4 = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CoatingProcessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoatingMeasurementSeriess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoatingMeasurementSeriess_CoatingProcess_CoatingProcessId",
                        column: x => x.CoatingProcessId,
                        principalTable: "CoatingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoatingMeasurementSeriess_CoatingProcessId",
                table: "CoatingMeasurementSeriess",
                column: "CoatingProcessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoatingMeasurementSeriess");

            migrationBuilder.DropTable(
                name: "Coatings");

            migrationBuilder.DropTable(
                name: "CoatingProcess");
        }
    }
}
