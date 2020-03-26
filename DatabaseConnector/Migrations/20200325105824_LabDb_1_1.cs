using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_1_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    BudgetId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<string>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    Used = table.Column<double>(nullable: false),
                    LabId = table.Column<int>(nullable: false),
                    LabName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "ChaimForm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFlowId = table.Column<long>(nullable: false),
                    LabId = table.Column<int>(nullable: false),
                    Applicant = table.Column<string>(nullable: true),
                    ChemicalId = table.Column<int>(nullable: false),
                    ReturnTime = table.Column<DateTime>(nullable: false),
                    RealReturnTime = table.Column<DateTime>(nullable: false),
                    Approver = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaimForm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclarationForm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFlowId = table.Column<long>(nullable: false),
                    LabId = table.Column<int>(nullable: false),
                    Applicant = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarationForm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialForm",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFlowId = table.Column<long>(nullable: false),
                    LabId = table.Column<int>(nullable: false),
                    Applicant = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Receiver = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialForm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMessage",
                columns: table => new
                {
                    NotificationMessageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimFormAck = table.Column<long>(nullable: false),
                    DeclarationFormAck = table.Column<long>(nullable: false),
                    FinancialFormAck = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessage", x => x.NotificationMessageId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    UserPassword = table.Column<string>(nullable: true),
                    LabId = table.Column<int>(nullable: false),
                    LabName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Applicant = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chemical",
                columns: table => new
                {
                    ChemicalId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    LabId = table.Column<int>(nullable: false),
                    WorkFlowId = table.Column<long>(nullable: false),
                    LabName = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    FactoryName = table.Column<string>(nullable: true),
                    ProductionTime = table.Column<DateTime>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    UnitMeasurement = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemical", x => x.ChemicalId);
                    table.ForeignKey(
                        name: "FK_Chemical_WorkFlow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "WorkFlow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimFormChemical",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimFormId = table.Column<long>(nullable: false),
                    ChemicalId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimFormChemical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimFormChemical_Chemical_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemical",
                        principalColumn: "ChemicalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimFormChemical_ChaimForm_ClaimFormId",
                        column: x => x.ClaimFormId,
                        principalTable: "ChaimForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget_LabId",
                table: "Budget",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_ChaimForm_LabId",
                table: "ChaimForm",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_ChaimForm_WorkFlowId",
                table: "ChaimForm",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Chemical_LabId",
                table: "Chemical",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_Chemical_WorkFlowId",
                table: "Chemical",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimFormChemical_ChemicalId",
                table: "ClaimFormChemical",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimFormChemical_ClaimFormId",
                table: "ClaimFormChemical",
                column: "ClaimFormId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationForm_LabId",
                table: "DeclarationForm",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationForm_WorkFlowId",
                table: "DeclarationForm",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialForm_LabId",
                table: "FinancialForm",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialForm_WorkFlowId",
                table: "FinancialForm",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessage_UserId",
                table: "NotificationMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "ClaimFormChemical");

            migrationBuilder.DropTable(
                name: "DeclarationForm");

            migrationBuilder.DropTable(
                name: "FinancialForm");

            migrationBuilder.DropTable(
                name: "NotificationMessage");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Chemical");

            migrationBuilder.DropTable(
                name: "ChaimForm");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WorkFlow");
        }
    }
}
