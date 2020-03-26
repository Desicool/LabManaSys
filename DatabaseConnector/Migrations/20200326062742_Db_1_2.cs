using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class Db_1_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChaimForm_WorkFlowId",
                table: "ChaimForm");

            migrationBuilder.DropColumn(
                name: "WorkFlowId",
                table: "ChaimForm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkFlowId",
                table: "ChaimForm",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ChaimForm_WorkFlowId",
                table: "ChaimForm",
                column: "WorkFlowId");
        }
    }
}
