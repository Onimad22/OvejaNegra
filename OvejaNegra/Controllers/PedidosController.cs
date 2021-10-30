using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OvejaNegra.Data;
using OvejaNegra.Data.Entities;
using OvejaNegra.Helpers;
using OvejaNegra.Models;

namespace OvejaNegra.Controllers
{
    public class PedidosController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHerlper;

        public PedidosController(DataContext context,ICombosHelper combosHerlper)
        {
            _context = context;
            _combosHerlper = combosHerlper;
        }

        // GET: Pedidos
        public IActionResult Index()
        {
            return View(_context.Pedidos.Include(p => p.Comandas).Where(m => m.Fecha == DateTime.Today).Where(c => c.Cerrado ==false));
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.Include(c=>c.Comandas).ThenInclude(p=>p.Producto).Include(e=>e.Comandas).ThenInclude(h=>h.Extra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mesa,Delivery,Pago,Cerrado")] Pedido pedido)
        {
            pedido.Fecha = DateTime.Today;
            pedido.Hora = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mesa,Fecha,Hora,Preparando,,Delivery,Pago,Cerrado")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }

        //ACA VAN LOS COMANDOS DE LAS COMANDAS

        public async Task<IActionResult> AddComanda(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id.Value);
            if (pedido == null)
            {
                return NotFound();
            }

            var model = new ComandaViewModel
            {
                PedidoId = pedido.Id,
                Productos = _combosHerlper.GetComboProducto(),
                PedidoDelivery = pedido.Delivery
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComanda(ComandaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comanda = new Comanda();
                comanda.Pedido = await _context.Pedidos.FindAsync(model.PedidoId);
                comanda.Cantidad = model.Cantidad;
                comanda.Producto = await _context.Productos.FindAsync(model.ProductoId);
                comanda.Carne = model.Carne;
                comanda.Papa = model.Papa;
                comanda.Queso = model.Queso;

                var categoria = comanda.Producto.Categoria;
                var cantidadCarne = comanda.Carne;
                var cantidadQueso = comanda.Queso;
                var cantidadPapa = comanda.Papa;

                if (categoria=="Hamburguesa")
                {
                    cantidadCarne = cantidadCarne - 1;
                    cantidadQueso = cantidadQueso - 1;
                }
                else
                {
                    comanda.Carne = 0;
                    comanda.Queso = 0;
                    comanda.Papa = 0;
                    cantidadCarne = 0;
                    cantidadQueso = 0;
                    cantidadPapa = 0;
                }
                
                var precio = 0.0;
                
                if (model.PedidoDelivery==true)
                {
                    precio = comanda.Producto.PrecioDelivery;
                }
                else
                {
                    precio = comanda.Producto.PrecioLocal;
                }

                var extras = _context.Extras;

                var carne = await extras.FindAsync(4);
                var ValorCarne = carne.Precio;

                var queso = await extras.FindAsync(3);
                var ValorQueso = queso.Precio;

                var papa = await extras.FindAsync(2);
                var ValorPapa = papa.Precio;

                comanda.Total = precio * model.Cantidad + cantidadCarne * ValorCarne + cantidadPapa * ValorPapa + cantidadQueso * ValorQueso;

                _context.Comandas.Add(comanda);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Pedidos", new { id = model.PedidoId });

            }
            return View(model);
        }


    }
}
