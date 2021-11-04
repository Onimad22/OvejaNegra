using Microsoft.AspNetCore.Mvc.Rendering;
using OvejaNegra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;

        }

        public IEnumerable<SelectListItem> GetComboEmpleado()
        {
            var lista = _context.Empleado.Select(p => new SelectListItem
            {
                Text = p.Nombre,
                Value = $"{p.Id}"
            })
              .OrderBy(p => p.Text)
              .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "Select a Item",
                Value = "0"
            });


            return lista;
        }

        public IEnumerable<SelectListItem> GetComboInsumo()
        {
            var lista = _context.Insumos.Select(p => new SelectListItem
            {
                Text = p.Nombre,
                Value = $"{p.Id}"
            })
               .OrderBy(p => p.Text)
               .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "Select a Item",
                Value = "0"
            });


            return lista;
        }

        public IEnumerable<SelectListItem> GetComboProducto()
        {
            var lista = _context.Productos.Select(p => new SelectListItem
            {
                Text = p.Nombre,
                Value = $"{p.Id}"
            })
                .OrderBy(p => p.Text)
                .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "Select a Item",
                Value = "0"
            });


            return lista;
        }
    }
}
