using Microsoft.EntityFrameworkCore.Migrations;

namespace UitgaveBeheer.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categorie",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Voedsel" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "School" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategorieId",
                table: "Expenses",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategorieId",
                table: "Expenses",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategorieId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CategorieId",
                table: "Expenses");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "Categorie",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
