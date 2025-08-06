using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PressingVisual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Markers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Markers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Markers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Markers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Markers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MarkerId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Markers_ImageId",
                table: "Markers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MarkerId",
                table: "Images",
                column: "MarkerId",
                unique: true,
                filter: "[MarkerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Markers_MarkerId",
                table: "Images",
                column: "MarkerId",
                principalTable: "Markers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Images_ImageId",
                table: "Markers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Markers_MarkerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Images_ImageId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Markers_ImageId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Images_MarkerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "MarkerId",
                table: "Images");
        }
    }
}
