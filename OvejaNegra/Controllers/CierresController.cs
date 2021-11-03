using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OvejaNegra.Data;
using OvejaNegra.Data.Entities;

namespace OvejaNegra.Controllers
{
    public class CierresController : Controller
    {
        private readonly DataContext _context;

        public CierresController(DataContext context)
        {
            _context = context;
        }

        // GET: Cierres
        public async Task<IActionResult> Index()
        {


            return View(await _context.Cierre.ToListAsync());
        }

        // GET: Cierres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cierre = await _context.Cierre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cierre == null)
            {
                return NotFound();
            }

            return View(cierre);
        }

        // GET: Cierres/Create
        public IActionResult Create()
        {
            var fechaHoy = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0)).Date;
            var fechaAyer = DateTimeOffset.Now.AddDays(-1).Date;

            var cajaA = new Caja { Total = 0 };
            var cajaH = new Caja { Total = 0 };
            var compras = 0.0;
            var ventas = 0.0;

            if (_context.Compras.FirstOrDefault(f => f.Fecha.Date == fechaHoy) != null)
            {
                var compra = _context.Compras.Where(f => f.Fecha.Date == fechaHoy);
                compras = compra.Sum(f => f.Total);
            }

            if (_context.Pedidos.FirstOrDefault(f => f.Fecha.Date == fechaHoy) != null)
            {
               var venta = _context.Pedidos.Where(f => f.Fecha.Date == fechaHoy);
                ventas = venta.Sum(s => s.Total);
            }

            if (_context.Caja.FirstOrDefault(f => f.Fecha.Date == fechaHoy) != null)
            {
                cajaH = _context.Caja.FirstOrDefault(f => f.Fecha.Date == fechaHoy);
            }

            if (_context.Caja.FirstOrDefault(f => f.Fecha.Date == fechaAyer) != null)
            {
                cajaA = _context.Caja.FirstOrDefault(f => f.Fecha.Date == fechaAyer);
            }

            var cierre = new Cierre();

            cierre.CajaAyer = cajaA.Total;
            cierre.CajaHoy = cajaH.Total;
            cierre.Ventas = ventas;
            cierre.Compras = compras;

            cierre.Fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0));
            cierre.Balance = (cierre.CajaAyer + cierre.Ventas - cierre.Compras - cierre.CajaHoy)*-1;

            return View(cierre);
        }

        // POST: Cierres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,CajaAyer,CajaHoy,Ventas,Compras,Balance")] Cierre cierre)
        {

            //var fechaHoy = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0)).Date;
            //var fechaAyer = DateTimeOffset.Now.AddDays(-1).Date;

            //var cajaAyer = await _context.Caja.FirstOrDefaultAsync(f => f.Fecha.Date == fechaAyer);
            //var cajaHoy = await _context.Caja.FirstOrDefaultAsync(f => f.Fecha.Date == fechaHoy);
            //var venta = await _context.Pedidos.FirstOrDefaultAsync(f => f.Fecha.Date == fechaHoy);
            //var compra = await _context.Compras.FirstOrDefaultAsync(f => f.Fecha.Date == fechaHoy);

            //cierre.CajaAyer = cajaAyer.Total;
            //cierre.CajaHoy = cajaHoy.Total;
            //cierre.Ventas = venta.Total;
            //cierre.Compras = compra.Total;

            //cierre.Balance = cierre.CajaAyer + cierre.Ventas - cierre.Compras - cierre.CajaHoy;
            //cierre.Fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0));
            //_context.Add(cierre);
            //await _context.SaveChangesAsync();


            if (ModelState.IsValid)
            {
                _context.Add(cierre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cierre);
        }

        // GET: Cierres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cierre = await _context.Cierre.FindAsync(id);
            if (cierre == null)
            {
                return NotFound();
            }
            return View(cierre);
        }

        // POST: Cierres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,CajaAyer,CajaHoy,Ventas,Compras,Balance")] Cierre cierre)
        {
            if (id != cierre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cierre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CierreExists(cierre.Id))
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
            return View(cierre);
        }

        // GET: Cierres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cierre = await _context.Cierre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cierre == null)
            {
                return NotFound();
            }

            return View(cierre);
        }

        // POST: Cierres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cierre = await _context.Cierre.FindAsync(id);
            _context.Cierre.Remove(cierre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CierreExists(int id)
        {
            return _context.Cierre.Any(e => e.Id == id);
        }
    }
}
