using Microsoft.AspNetCore.Mvc.Rendering;
using OvejaNegra.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Models
{
    public class ComandaViewModel:Comanda
    {
        public int PedidoId { get; set; }

        
        [Display(Name = "Producto")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Item")]
        public int ProductoId { get; set; }

        public IEnumerable<SelectListItem> Productos { get; set; }

        public bool PedidoDelivery{ get; set; }
    }
}
