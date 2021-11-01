using OvejaNegra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Mesa")]
        public string Mesa { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }

        public bool Pago { get; set; }

        public bool Cerrado { get; set; }

        public bool Preparando { get; set; }

        public bool Delivery { get; set; }

        public ICollection<Comanda> Comandas { get; set; }
    }
}
