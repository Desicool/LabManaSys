using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_1_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Applicant",
                table: "WorkFlow");

            migrationBuilder.DropColumn(
                name: "Applicant",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "Applicant",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "Applicant",
                table: "ChaimForm");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FinancialForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DeclarationForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ChaimForm",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChaimForm");

            migrationBuilder.AddColumn<string>(
                name: "Applicant",
                table: "WorkFlow",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Applicant",
                table: "FinancialForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Applicant",
                table: "DeclarationForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Applicant",
                table: "ChaimForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
