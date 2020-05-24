using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mokki.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Anfitrions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pueblo = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anfitrions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anfitrions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Huesped",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ciudad = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huesped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huesped_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estancia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duracion = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    AnfitrionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estancia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estancia_Anfitrions_AnfitrionId",
                        column: x => x.AnfitrionId,
                        principalTable: "Anfitrions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstanciaHuesped",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstanciaId = table.Column<int>(nullable: false),
                    HuespedId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstanciaHuesped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstanciaHuesped_Estancia_EstanciaId",
                        column: x => x.EstanciaId,
                        principalTable: "Estancia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstanciaHuesped_Huesped_HuespedId",
                        column: x => x.HuespedId,
                        principalTable: "Huesped",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anfitrions_UserId",
                table: "Anfitrions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Estancia_AnfitrionId",
                table: "Estancia",
                column: "AnfitrionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstanciaHuesped_EstanciaId",
                table: "EstanciaHuesped",
                column: "EstanciaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstanciaHuesped_HuespedId",
                table: "EstanciaHuesped",
                column: "HuespedId");

            migrationBuilder.CreateIndex(
                name: "IX_Huesped_UserId",
                table: "Huesped",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstanciaHuesped");

            migrationBuilder.DropTable(
                name: "Estancia");

            migrationBuilder.DropTable(
                name: "Huesped");

            migrationBuilder.DropTable(
                name: "Anfitrions");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "AspNetUsers");
        }
    }
}
