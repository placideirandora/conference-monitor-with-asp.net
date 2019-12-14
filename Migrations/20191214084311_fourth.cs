using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api_with_asp.net.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateAndTime",
                table: "Conferences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateAndTime",
                table: "Conferences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateAndTime",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "StartDateAndTime",
                table: "Conferences");
        }
    }
}
