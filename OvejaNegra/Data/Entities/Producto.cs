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
        [Display(Name = "Precio Delivery")]
        public double PrecioDelivery { get; set; }

        [Required]
        public int Carne { get; set; }

        [Required]
        public int Papa { get; set; }

        [Required]
        public double Bono { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        public ICollection<Comanda> Comandas { get; set; }
    }
}
