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

        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }

        [DataType(DataType.Time)]
        public DateTimeOffset Hora => Fecha;

        public bool Pago { get; set; }

        public bool Cerrado { get; set; }

        public bool Preparando { get; set; }

        public bool Delivery { get; set; }

        public double Total { get; set; }

        public double BonoT { get; set; }

        public ICollection<Comanda> Comandas { get; set; }
    }
}
