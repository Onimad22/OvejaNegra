using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class SueldoPago
    {
        [Key]
        public int Id { get; set; }


        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }

        public double Total { get; set; }

        public Empleado Empleado { get; set; }
    }
}
