using Microsoft.EntityFrameworkCore.Migrations;

namespace NUEVO.EmlakOfisi.Case.Data.Migrations
{
    public partial class initv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GorselLinki",
                table: "Ilans",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GorselLinki",
                table: "Ilans");
        }
    }
}
