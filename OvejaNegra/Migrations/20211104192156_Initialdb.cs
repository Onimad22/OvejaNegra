using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OvejaNegra.Migrations
{
    public partial class Initialdb : Migration
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
                    doscientosb = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sueldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mesa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Pago = table.Column<bool>(type: "bit", nullable: false),
                    Cerrado = table.Column<bool>(type: "bit", nullable: false),
                    Preparando = table.Column<bool>(type: "bit", nullable: false),
                    Delivery = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    BonoT = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioLocal = table.Column<double>(type: "float", nullable: false),
                    PrecioDelivery = table.Column<double>(type: "float", nullable: false),
                    Carne = table.Column<int>(type: "int", nullable: false),
                    Papa = table.Column<int>(type: "int", nullable: false),
                    Bono = table.Column<double>(type: "float", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sueldo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    HoraE = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    HoraS = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    HoraT = table.Column<double>(type: "float", nullable: false),
                    Jornal = table.Column<double>(type: "float", nullable: false),
                    Bono = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Pago = table.Column<bool>(type: "bit", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sueldo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sueldo_Empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    InsumoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Insumos_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    Vegetariana = table.Column<bool>(type: "bit", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Bono = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comandas_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comandas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_PedidoId",
                table: "Comandas",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_ProductoId",
                table: "Comandas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_InsumoId",
                table: "Compras",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sueldo_EmpleadoId",
                table: "Sueldo",
                column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caja");

            migrationBuilder.DropTable(
                name: "Cierre");

            migrationBuilder.DropTable(
                name: "Comandas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Sueldo");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Empleado");
        }
    }
}
