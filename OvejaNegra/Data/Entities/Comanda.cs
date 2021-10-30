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

        [Required]
        public int Cantidad { get; set; }

        
        public Producto Producto { get; set; }

        public Extra Extra { get; set; }

        public int Carne { get; set; }
        public int Papa { get; set; }
        public int Queso { get; set; }

        public double Total { get; set; }
    }
}
