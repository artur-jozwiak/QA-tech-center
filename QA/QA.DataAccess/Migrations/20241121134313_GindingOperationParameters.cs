using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GindingOperationParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cassette",
                table: "OperationDetails",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CassetteInsertsQty",
                table: "OperationDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CentralTableDirection",
                table: "OperationDetails",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CentralTableRPM",
                table: "OperationDetails",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LowerDirection",
                table: "OperationDetails",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LowerRPM",
                table: "OperationDetails",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "OperationDetails",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderKey",
                table: "OperationDetails",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpperDirection",
                table: "OperationDetails",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UpperRPM",
                table: "OperationDetails",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cassette",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "CassetteInsertsQty",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "CentralTableDirection",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "CentralTableRPM",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "LowerDirection",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "LowerRPM",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "OrderKey",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "UpperDirection",
                table: "OperationDetails");

            migrationBuilder.DropColumn(
                name: "UpperRPM",
                table: "OperationDetails");
        }
    }
}
