using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant.Migrations
{
    public partial class editDb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "TransactionContactUs");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "TransactionContactUs");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "TransactionContactUs");

            migrationBuilder.DropColumn(
                name: "EditUser",
                table: "TransactionContactUs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TransactionContactUs");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "TransactionContactUs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "TransactionContactUs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "TransactionContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "TransactionContactUs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditUser",
                table: "TransactionContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TransactionContactUs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "TransactionContactUs",
                type: "bit",
                nullable: true);
        }
    }
}
