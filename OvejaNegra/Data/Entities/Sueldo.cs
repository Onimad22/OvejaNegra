using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Sueldo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }

        [Display(Name = "Entrada")]
        [DataType(DataType.Time)]
        public DateTimeOffset HoraE { get; set; }

        [Display(Name = "Salida")]
        [DataType(DataType.Time)]
        public DateTimeOffset HoraS { get; set; }

        [Display(Name = "Horas")]
        public double HoraT { get; set; }

        [Display(Name = "Sueldo")]
        public double Jornal { get; set; }

        public double Bono { get; set; }

        public double Total { get; set; }

        public bool Pago { get; set; }

        public Empleado Empleado { get; set; }

    }
}
