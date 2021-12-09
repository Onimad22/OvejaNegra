using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Cierre
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }

        [Display(Name = "Caja (Ayer)")]
        public double CajaAyer { get; set; }

        [Display(Name = "Caja (Hoy)")]
        public double CajaHoy { get; set; }

        [Display(Name = "Ventas")]
        public double Ventas { get; set; }

        [Display(Name = "Compras")]
        public double Compras { get; set; }

        [Display(Name = "Sueldos")]
        public double Sueldos { get; set; }

        [Display(Name = "Cierre")]
        public double Balance { get; set; }

        public double Bono { get; set; }

    }
}
