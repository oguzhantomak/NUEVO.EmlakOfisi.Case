using Microsoft.EntityFrameworkCore.Migrations;

namespace NUEVO.EmlakOfisi.Case.Data.Migrations
{
    public partial class initv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilans_EmlakTuru_EmlakTuruId",
                table: "Ilans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmlakTuru",
                table: "EmlakTuru");

            migrationBuilder.RenameTable(
                name: "EmlakTuru",
                newName: "EmlakTurus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmlakTurus",
                table: "EmlakTurus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ilans_EmlakTurus_EmlakTuruId",
                table: "Ilans",
                column: "EmlakTuruId",
                principalTable: "EmlakTurus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilans_EmlakTurus_EmlakTuruId",
                table: "Ilans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmlakTurus",
                table: "EmlakTurus");

            migrationBuilder.RenameTable(
                name: "EmlakTurus",
                newName: "EmlakTuru");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmlakTuru",
                table: "EmlakTuru",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ilans_EmlakTuru_EmlakTuruId",
                table: "Ilans",
                column: "EmlakTuruId",
                principalTable: "EmlakTuru",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
