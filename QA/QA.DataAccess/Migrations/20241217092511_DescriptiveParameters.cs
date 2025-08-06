using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DescriptiveParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptiveParameterId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DescriptiveParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TestingInstrument = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FillingMethod = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptiveParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptiveParameter_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderKey = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_DescriptiveParameter_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "DescriptiveParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_DescriptiveParameterId",
                table: "Images",
                column: "DescriptiveParameterId",
                unique: true,
                filter: "[DescriptiveParameterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptiveParameter_OperationId",
                table: "DescriptiveParameter",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_OrderId",
                table: "Results",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ParameterId",
                table: "Results",
                column: "ParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_DescriptiveParameter_DescriptiveParameterId",
                table: "Images",
                column: "DescriptiveParameterId",
                principalTable: "DescriptiveParameter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_DescriptiveParameter_DescriptiveParameterId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "DescriptiveParameter");

            migrationBuilder.DropIndex(
                name: "IX_Images_DescriptiveParameterId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DescriptiveParameterId",
                table: "Images");
        }
    }
}
