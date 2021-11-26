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
    public class TransferenciasController : Controller
    {
        private readonly DataContext _context;

        public TransferenciasController(DataContext context)
        {
            _context = context;
        }

        // GET: Transferencias
        public async Task<IActionResult> Index()
        {
            var fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0)).Date;

            return View(await _context.Transferencia.Where(f => f.Fecha.Date == fecha).ToListAsync());

        }

        // GET: Transferencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // GET: Transferencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transferencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total")] Transferencia transferencia)
        {
            var fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0)).Date;

            if (ModelState.IsValid)
            {
                transferencia.Fecha = fecha;
                _context.Add(transferencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transferencia);
        }

        // GET: Transferencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencia.FindAsync(id);
            if (transferencia == null)
            {
                return NotFound();
            }
            return View(transferencia);
        }

        // POST: Transferencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total")] Transferencia transferencia)
        {
            if (id != transferencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferenciaExists(transferencia.Id))
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
            return View(transferencia);
        }

        // GET: Transferencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // POST: Transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transferencia = await _context.Transferencia.FindAsync(id);
            _context.Transferencia.Remove(transferencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferenciaExists(int id)
        {
            return _context.Transferencia.Any(e => e.Id == id);
        }
    }
}
