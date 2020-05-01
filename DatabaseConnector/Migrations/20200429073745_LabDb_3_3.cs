using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_3_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimFormChemical_ChaimForm_ClaimFormId",
                table: "ClaimFormChemical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChaimForm",
                table: "ChaimForm");

            migrationBuilder.RenameTable(
                name: "ChaimForm",
                newName: "ClaimForm");

            migrationBuilder.RenameIndex(
                name: "IX_ChaimForm_LabId",
                table: "ClaimForm",
                newName: "IX_ClaimForm_LabId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimForm",
                table: "ClaimForm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimFormChemical_ClaimForm_ClaimFormId",
                table: "ClaimFormChemical",
                column: "ClaimFormId",
                principalTable: "ClaimForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimFormChemical_ClaimForm_ClaimFormId",
                table: "ClaimFormChemical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimForm",
                table: "ClaimForm");

            migrationBuilder.RenameTable(
                name: "ClaimForm",
                newName: "ChaimForm");

            migrationBuilder.RenameIndex(
                name: "IX_ClaimForm_LabId",
                table: "ChaimForm",
                newName: "IX_ChaimForm_LabId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChaimForm",
                table: "ChaimForm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimFormChemical_ChaimForm_ClaimFormId",
                table: "ClaimFormChemical",
                column: "ClaimFormId",
                principalTable: "ChaimForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
