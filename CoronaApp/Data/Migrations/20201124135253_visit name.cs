using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaApp.Data.Migrations
{
    public partial class visitname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Visits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Visits");
        }
    }
}
