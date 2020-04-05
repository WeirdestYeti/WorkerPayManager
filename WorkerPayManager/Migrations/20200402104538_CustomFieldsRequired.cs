using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerPayManager.Migrations
{
    public partial class CustomFieldsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Workers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "CustomWorkerFields",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "CustomWorkerFields");
        }
    }
}
