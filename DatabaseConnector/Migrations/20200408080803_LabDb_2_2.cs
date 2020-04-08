using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnector.Migrations
{
    public partial class LabDb_2_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "FinancialForm",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "FinancialForm",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "DeclarationForm",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "DeclarationForm",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "ChaimForm",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ChaimForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "FinancialForm");

            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "DeclarationForm");

            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "ChaimForm");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ChaimForm");
        }
    }
}
