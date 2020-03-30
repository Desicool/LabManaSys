using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_2_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabId",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "FinancialForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "DeclarationForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealReturnTime",
                table: "ChaimForm",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ChaimForm",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "State",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "State",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ChaimForm");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealReturnTime",
                table: "ChaimForm",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
