using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_2_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "ChaimForm");

            migrationBuilder.AddColumn<int>(
                name: "HandlerId",
                table: "FinancialForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HandlerId",
                table: "DeclarationForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HandlerId",
                table: "ChaimForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "ChaimForm");

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "FinancialForm",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "DeclarationForm",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "ChaimForm",
                type: "int",
                nullable: true);
        }
    }
}
