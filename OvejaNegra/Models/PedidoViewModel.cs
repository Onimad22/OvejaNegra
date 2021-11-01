using OvejaNegra.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Models
{
    public class PedidoViewModel:ComandaViewModel
    {
        [Required]
        [Display(Name = "Mesa")]
        public string Mesa { get; set; }

        public bool Delivery { get; set; }

        public ICollection<Comanda> Comandas { get; set; }
    }
}
