using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChildParameterAssignementConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParameterType",
                table: "Parameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChildParametersAssignement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterOrder = table.Column<int>(type: "int", nullable: false),
                    ChildParameterId = table.Column<int>(type: "int", nullable: true),
                    ParentParameterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildParametersAssignement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildParametersAssignement_Parameters_ChildParameterId",
                        column: x => x.ChildParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildParametersAssignement_Parameters_ParentParameterId",
                        column: x => x.ParentParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildParametersAssignement_ChildParameterId",
                table: "ChildParametersAssignement",
                column: "ChildParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildParametersAssignement_ParentParameterId",
                table: "ChildParametersAssignement",
                column: "ParentParameterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildParametersAssignement");

            migrationBuilder.DropColumn(
                name: "ParameterType",
                table: "Parameters");
        }
    }
}
