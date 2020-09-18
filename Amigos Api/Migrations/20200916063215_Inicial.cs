using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Amigos_Api.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amigos",
                columns: table => new
                {
                    AmigoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Aniversario = table.Column<DateTime>(nullable: false),
                    PaisOrigem = table.Column<string>(nullable: true),
                    EstadoOrigem = table.Column<string>(nullable: true),
                    AmigoId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigos", x => x.AmigoId);
                    table.ForeignKey(
                        name: "FK_Amigos_Amigos_AmigoId1",
                        column: x => x.AmigoId1,
                        principalTable: "Amigos",
                        principalColumn: "AmigoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_AmigoId1",
                table: "Amigos",
                column: "AmigoId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amigos");
        }
    }
}
