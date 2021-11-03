using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OvejaNegra.Migrations
{
    public partial class nueva3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cierre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CajaAyer = table.Column<double>(type: "float", nullable: false),
                    CajaHoy = table.Column<double>(type: "float", nullable: false),
                    Ventas = table.Column<double>(type: "float", nullable: false),
                    Compras = table.Column<double>(type: "float", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cierre", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cierre");
        }
    }
}
