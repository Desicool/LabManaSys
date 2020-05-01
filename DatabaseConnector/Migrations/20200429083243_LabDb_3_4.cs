using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_3_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusChangeMessage_WorkFlow_WorkFlowId",
                table: "StatusChangeMessage");

            migrationBuilder.DropIndex(
                name: "IX_StatusChangeMessage_WorkFlowId",
                table: "StatusChangeMessage");

            migrationBuilder.DropColumn(
                name: "WorkFlowId",
                table: "StatusChangeMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkFlowId",
                table: "StatusChangeMessage",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeMessage_WorkFlowId",
                table: "StatusChangeMessage",
                column: "WorkFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusChangeMessage_WorkFlow_WorkFlowId",
                table: "StatusChangeMessage",
                column: "WorkFlowId",
                principalTable: "WorkFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
