using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Helicheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NarrowingLTol",
                table: "Products",
                type: "decimal(5,3)",
                precision: 5,
                scale: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NarrowingUTol",
                table: "Products",
                type: "decimal(5,3)",
                precision: 5,
                scale: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NarrowingLTol",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NarrowingUTol",
                table: "Products");
        }
    }
}
