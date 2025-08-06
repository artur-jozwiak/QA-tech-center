using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EdgesNoRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EdgeNo",
                table: "Parameters",
                newName: "EdgesQty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EdgesQty",
                table: "Parameters",
                newName: "EdgeNo");
        }
    }
}
