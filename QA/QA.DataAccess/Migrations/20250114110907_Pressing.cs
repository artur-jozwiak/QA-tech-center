using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Pressing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pressing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OrderKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrialNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PDMNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Height1 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    Height2 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    Height3 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    Height4 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    Force = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    UCSB = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    UPS = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    PrecompactingA = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    PrecompactingB = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    PressStrokeRelation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Decopression1A = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    Decopression1B = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    DecopressionV1 = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    Decopression2A = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    Decopression2B = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    DecopressionV2 = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    UnderfillStrokeB = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true),
                    SuctionFill = table.Column<bool>(type: "bit", nullable: false),
                    CounturFilling = table.Column<bool>(type: "bit", nullable: false),
                    TrayQty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BaloonNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RobotProgam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BurringPrassuereCloseValve = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BurringPrassuereOpenValve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Powder = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pressing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pressing_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pressing_OrderId",
                table: "Pressing",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pressing");
        }
    }
}
