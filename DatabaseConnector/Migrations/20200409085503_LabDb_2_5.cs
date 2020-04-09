using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_2_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HandlerName",
                table: "FinancialForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandlerName",
                table: "DeclarationForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandlerName",
                table: "ChaimForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandlerName",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "HandlerName",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "HandlerName",
                table: "ChaimForm");
        }
    }
}
