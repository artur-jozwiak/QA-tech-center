using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PowderSpecificationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowdersSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PowderType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HCJMin = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    HCJNominal = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    HCJMax = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    COMMin = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    COMNominal = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    COMMax = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    DensityMin = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    DensityNominal = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    DensityMax = table.Column<decimal>(type: "decimal(7,3)", nullable: true),
                    HV30Min = table.Column<int>(type: "int", nullable: true),
                    HV30Nominal = table.Column<int>(type: "int", nullable: true),
                    HV30Max = table.Column<int>(type: "int", nullable: true),
                    K1C = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Porosity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReleaseRules = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReleaseRulesForSamples = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowdersSpecifications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowdersSpecifications");
        }
    }
}
