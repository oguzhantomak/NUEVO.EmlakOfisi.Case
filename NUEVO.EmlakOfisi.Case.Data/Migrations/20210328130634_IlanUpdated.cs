using Microsoft.EntityFrameworkCore.Migrations;

namespace NUEVO.EmlakOfisi.Case.Data.Migrations
{
    public partial class IlanUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmlakYasi",
                table: "Ilans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Metrekare",
                table: "Ilans",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmlakYasi",
                table: "Ilans");

            migrationBuilder.DropColumn(
                name: "Metrekare",
                table: "Ilans");
        }
    }
}
