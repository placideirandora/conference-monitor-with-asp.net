using Microsoft.EntityFrameworkCore.Migrations;

namespace api_with_asp.net.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Conferences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                table: "Conferences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Conferences",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Organizer",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Conferences");
        }
    }
}
