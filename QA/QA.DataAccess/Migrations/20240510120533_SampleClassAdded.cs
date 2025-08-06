using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SampleClassAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SampleClass",
                table: "Parameters",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SampleClass",
                table: "Parameters");
        }
    }
}
