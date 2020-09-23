using Microsoft.EntityFrameworkCore.Migrations;

namespace Paises_Api.Migrations
{
    public partial class Imagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemPais",
                table: "Paises",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemEstado",
                table: "Estados",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemPais",
                table: "Paises");

            migrationBuilder.DropColumn(
                name: "ImagemEstado",
                table: "Estados");
        }
    }
}
