using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MRB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MRBId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MRBId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MRB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NonConformanceDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RootCause = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MRBDipositions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MRBCorrectiveActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StaffResponsible = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MRBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRBCorrectiveActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MRBCorrectiveActions_MRB_MRBId",
                        column: x => x.MRBId,
                        principalTable: "MRB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MRBInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StaffResponsible = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MRBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRBInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MRBInstructions_MRB_MRBId",
                        column: x => x.MRBId,
                        principalTable: "MRB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MRBMemberSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Member = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Approve = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MRBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRBMemberSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MRBMemberSummaries_MRB_MRBId",
                        column: x => x.MRBId,
                        principalTable: "MRB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MRBId",
                table: "Orders",
                column: "MRBId",
                unique: true,
                filter: "[MRBId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MRBId",
                table: "Images",
                column: "MRBId");

            migrationBuilder.CreateIndex(
                name: "IX_MRBCorrectiveActions_MRBId",
                table: "MRBCorrectiveActions",
                column: "MRBId");

            migrationBuilder.CreateIndex(
                name: "IX_MRBInstructions_MRBId",
                table: "MRBInstructions",
                column: "MRBId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MRBMemberSummaries_MRBId",
                table: "MRBMemberSummaries",
                column: "MRBId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_MRB_MRBId",
                table: "Images",
                column: "MRBId",
                principalTable: "MRB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MRB_MRBId",
                table: "Orders",
                column: "MRBId",
                principalTable: "MRB",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_MRB_MRBId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MRB_MRBId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "MRBCorrectiveActions");

            migrationBuilder.DropTable(
                name: "MRBInstructions");

            migrationBuilder.DropTable(
                name: "MRBMemberSummaries");

            migrationBuilder.DropTable(
                name: "MRB");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MRBId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Images_MRBId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MRBId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MRBId",
                table: "Images");
        }
    }
}
