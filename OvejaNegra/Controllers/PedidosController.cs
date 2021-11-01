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
            return View(_context.Pedidos.Include(p => p.Comandas).Where(c => c.Cerrado ==false));
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.Include(c=>c.Comandas).ThenInclude(p=>p.Producto).FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Mesa,Delivery")] Pedido model)
        {

            var fecha = DateTimeOffset.Now;
            var fechalocal = fecha.ToOffset(new TimeSpan(-4, 0, 0));
            
            model.Fecha = fechalocal;
            
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Pedidos", new { id = model.Id });
                //return RedirectToAction(nameof(Index));
            }
            return View(model);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mesa,Fecha,Hora,Preparando,Delivery,Pago,Cerrado")] Pedido pedido)
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
                comanda.Vegetariana = model.Vegetariana;
                comanda.Comentarios = model.Comentarios;
                
                var precio = 0.0;
                
                if (model.PedidoDelivery==true)
                {
                    precio = comanda.Producto.PrecioDelivery;
                }
                else
                {
                    precio = comanda.Producto.PrecioLocal;
                }


                comanda.Total = precio * model.Cantidad;

                _context.Comandas.Add(comanda);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Pedidos", new { id = model.PedidoId });

            }
            return View(model);
        }


        // GET: Comandas/Delete/5
        public async Task<IActionResult> DeleteComanda(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comandas.Include(c => c.Producto).FirstOrDefaultAsync(m => m.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // POST: Comandas/Delete/5
        [HttpPost, ActionName("DeleteComanda")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComandaConfirmed(int id)
        {
            var comanda = await _context.Comandas.Include(c=>c.Pedido).FirstOrDefaultAsync(m => m.Id == id);
            _context.Comandas.Remove(comanda);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Pedidos", new { id = comanda.Pedido.Id });

        }

    }


}

