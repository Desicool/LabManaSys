using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_1_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WorkFlow",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkFlow");
        }
    }
}
