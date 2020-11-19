using Microsoft.EntityFrameworkCore.Migrations;

namespace UitgaveBeheer.Migrations
{
    public partial class addactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actor",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actor",
                table: "Expenses");
        }
    }
}
