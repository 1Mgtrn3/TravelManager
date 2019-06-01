using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelManager.Migrations
{
    public partial class CurrenciesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Currencies",
                nullable: true,
                oldClrType: typeof(char));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Currencies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Currencies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
