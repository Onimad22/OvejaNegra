using System;
using System.ComponentModel.DataAnnotations;

namespace OvejaNegra.Data.Entities
{
    public class Transferencia
    {
        [Key]
        public int Id { get; set; }

        public double Total { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }
    }
}
