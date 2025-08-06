using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QA.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MRBCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MRBDipositions",
                table: "MRB");

            migrationBuilder.RenameColumn(
                name: "Approve",
                table: "MRBMemberSummaries",
                newName: "NotificationReceived");

            migrationBuilder.RenameColumn(
                name: "ApprovalDate",
                table: "MRBMemberSummaries",
                newName: "ModificationDate");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "MRBMemberSummaries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MRBDipositions",
                table: "MRBMemberSummaries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PositionInQueue",
                table: "MRBMemberSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "MRB",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MRB",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "MRBMemberSummaries");

            migrationBuilder.DropColumn(
                name: "MRBDipositions",
                table: "MRBMemberSummaries");

            migrationBuilder.DropColumn(
                name: "PositionInQueue",
                table: "MRBMemberSummaries");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "MRB");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MRB");

            migrationBuilder.RenameColumn(
                name: "NotificationReceived",
                table: "MRBMemberSummaries",
                newName: "Approve");

            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "MRBMemberSummaries",
                newName: "ApprovalDate");

            migrationBuilder.AddColumn<string>(
                name: "MRBDipositions",
                table: "MRB",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
