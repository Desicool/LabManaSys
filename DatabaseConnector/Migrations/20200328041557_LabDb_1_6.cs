using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_1_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approver",
                table: "ChaimForm");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FinancialForm",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "FinancialForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "DeclarationForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "ChaimForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FinancialForm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Approver",
                table: "ChaimForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
