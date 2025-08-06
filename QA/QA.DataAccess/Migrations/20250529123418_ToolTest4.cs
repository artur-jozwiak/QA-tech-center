using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ToolTest4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ComparePoints",
                newName: "Remarks");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ComparePoints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ControlPointValue",
                table: "ComparePoints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parameter",
                table: "ComparePoints",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlPointValue",
                table: "ComparePoints");

            migrationBuilder.DropColumn(
                name: "Parameter",
                table: "ComparePoints");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "ComparePoints",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ComparePoints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
