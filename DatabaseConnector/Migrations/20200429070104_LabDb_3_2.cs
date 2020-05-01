using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_3_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeMessage_RelatedType",
                table: "StatusChangeMessage",
                column: "RelatedType");

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeMessage_UserId",
                table: "StatusChangeMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessage_FormType",
                table: "NotificationMessage",
                column: "FormType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatusChangeMessage_RelatedType",
                table: "StatusChangeMessage");

            migrationBuilder.DropIndex(
                name: "IX_StatusChangeMessage_UserId",
                table: "StatusChangeMessage");

            migrationBuilder.DropIndex(
                name: "IX_NotificationMessage_FormType",
                table: "NotificationMessage");
        }
    }
}
