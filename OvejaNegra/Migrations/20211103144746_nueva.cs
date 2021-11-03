using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OvejaNegra.Migrations
{
    public partial class nueva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    diezc = table.Column<int>(type: "int", nullable: false),
                    veintec = table.Column<int>(type: "int", nullable: false),
                    cincuentac = table.Column<int>(type: "int", nullable: false),
                    unb = table.Column<int>(type: "int", nullable: false),
                    dosb = table.Column<int>(type: "int", nullable: false),
                    cincob = table.Column<int>(type: "int", nullable: false),
                    diezb = table.Column<int>(type: "int", nullable: false),
                    veinteb = table.Column<int>(type: "int", nullable: false),
                    cincuentab = table.Column<int>(type: "int", nullable: false),
                    cienb = table.Column<int>(type: "int", nullable: false),
                    doscientosb = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caja");
        }
    }
}
