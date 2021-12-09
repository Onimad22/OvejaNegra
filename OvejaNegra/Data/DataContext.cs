using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OvejaNegra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data
{
    public class DataContext:IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Caja> Caja { get; set; }
        public DbSet<Cierre> Cierre { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Sueldo> Sueldo { get; set; }
        public DbSet<SueldoPago> SueldosPago { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transferencia> Transferencia { get; set; }


    }

}
