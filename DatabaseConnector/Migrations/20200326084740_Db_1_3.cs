using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class Db_1_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotificationMessage_UserId",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "ClaimFormAck",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "DeclarationFormAck",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "FinancialFormAck",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "ChemicalId",
                table: "ChaimForm");

            migrationBuilder.AddColumn<long>(
                name: "FormId",
                table: "NotificationMessage",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "FormType",
                table: "NotificationMessage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSolved",
                table: "NotificationMessage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "NotificationMessage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessage_RoleId",
                table: "NotificationMessage",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationMessage_Role_RoleId",
                table: "NotificationMessage",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationMessage_Role_RoleId",
                table: "NotificationMessage");

            migrationBuilder.DropIndex(
                name: "IX_NotificationMessage_RoleId",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "FormType",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "IsSolved",
                table: "NotificationMessage");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "NotificationMessage");

            migrationBuilder.AddColumn<long>(
                name: "ClaimFormAck",
                table: "NotificationMessage",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeclarationFormAck",
                table: "NotificationMessage",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FinancialFormAck",
                table: "NotificationMessage",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "NotificationMessage",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChemicalId",
                table: "ChaimForm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessage_UserId",
                table: "NotificationMessage",
                column: "UserId");
        }
    }
}
