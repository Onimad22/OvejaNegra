using Microsoft.EntityFrameworkCore;
using OvejaNegra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<OvejaNegra.Data.Entities.Caja> Caja { get; set; }

    }
   
}
