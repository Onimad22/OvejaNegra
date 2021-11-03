﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OvejaNegra.Data;

namespace OvejaNegra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211103144746_nueva")]
    partial class nueva
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OvejaNegra.Data.Entities.Caja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Fecha")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("cienb")
                        .HasColumnType("int");

                    b.Property<int>("cincob")
                        .HasColumnType("int");

                    b.Property<int>("cincuentab")
                        .HasColumnType("int");

                    b.Property<int>("cincuentac")
                        .HasColumnType("int");

                    b.Property<int>("diezb")
                        .HasColumnType("int");

                    b.Property<int>("diezc")
                        .HasColumnType("int");

                    b.Property<int>("dosb")
                        .HasColumnType("int");

                    b.Property<int>("doscientosb")
                        .HasColumnType("int");

                    b.Property<int>("unb")
                        .HasColumnType("int");

                    b.Property<int>("veinteb")
                        .HasColumnType("int");

                    b.Property<int>("veintec")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Caja");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<bool>("Vegetariana")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Fecha")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("InsumoId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InsumoId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Insumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Insumos");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cerrado")
                        .HasColumnType("bit");

                    b.Property<bool>("Delivery")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("Fecha")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Mesa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Pago")
                        .HasColumnType("bit");

                    b.Property<bool>("Preparando")
                        .HasColumnType("bit");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Bono")
                        .HasColumnType("float");

                    b.Property<int>("Carne")
                        .HasColumnType("int");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Papa")
                        .HasColumnType("int");

                    b.Property<double>("PrecioDelivery")
                        .HasColumnType("float");

                    b.Property<double>("PrecioLocal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Comanda", b =>
                {
                    b.HasOne("OvejaNegra.Data.Entities.Pedido", "Pedido")
                        .WithMany("Comandas")
                        .HasForeignKey("PedidoId");

                    b.HasOne("OvejaNegra.Data.Entities.Producto", "Producto")
                        .WithMany("Comandas")
                        .HasForeignKey("ProductoId");

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Compra", b =>
                {
                    b.HasOne("OvejaNegra.Data.Entities.Insumo", "Insumo")
                        .WithMany("Compras")
                        .HasForeignKey("InsumoId");

                    b.Navigation("Insumo");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Insumo", b =>
                {
                    b.Navigation("Compras");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Pedido", b =>
                {
                    b.Navigation("Comandas");
                });

            modelBuilder.Entity("OvejaNegra.Data.Entities.Producto", b =>
                {
                    b.Navigation("Comandas");
                });
#pragma warning restore 612, 618
        }
    }
}
