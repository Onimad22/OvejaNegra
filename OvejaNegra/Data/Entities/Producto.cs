using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Data.Entities
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Precio Local")]
        public double PrecioLocal { get; set; }
        [Required]
        [Display(Name = "Precio Mesa")]
        public double PrecioDelivery { get; set; }
    }
}
