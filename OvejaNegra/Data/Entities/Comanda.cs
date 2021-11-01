using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Comanda
    {
        [Key]
        public int Id { get; set; }
        
        public Pedido Pedido { get; set; }

        
        public int Cantidad { get; set; }
        
        public Producto Producto { get; set; }

        public bool Vegetariana { get; set; }

        public string Comentarios { get; set; }

        public double Total { get; set; }
    }
}
