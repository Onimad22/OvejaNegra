using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "$/H")]
        public double Sueldo { get; set; }

        public ICollection<Sueldo> Sueldos { get; set; }
    }
}
