using Microsoft.EntityFrameworkCore.Migrations;

namespace api_with_asp.net.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    StartDateAndTime = table.Column<string>(nullable: true),
                    EndDateAndTime = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Organizer = table.Column<string>(nullable: true),
                    TicketPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conferences");
        }
    }
}
