using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_3_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkFlowStatusChangeMessage");

            migrationBuilder.CreateTable(
                name: "StatusChangeMessage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedId = table.Column<long>(nullable: false),
                    RelatedType = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    WorkFlowId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusChangeMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusChangeMessage_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "WorkFlow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeMessage_WorkFlowId",
                table: "StatusChangeMessage",
                column: "WorkFlowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusChangeMessage");

            migrationBuilder.CreateTable(
                name: "WorkFlowStatusChangeMessage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkFlowId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowStatusChangeMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowStatusChangeMessage_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "WorkFlow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStatusChangeMessage_WorkFlowId",
                table: "WorkFlowStatusChangeMessage",
                column: "WorkFlowId");
        }
    }
}
