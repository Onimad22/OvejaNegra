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
    public class CajasController : Controller
    {
        private readonly DataContext _context;

        public CajasController(DataContext context)
        {
            _context = context;
        }

        // GET: Cajas
        public async Task<IActionResult> Index()
        {
            var fechaHoy = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0)).Date;
            var fechaAyer = DateTimeOffset.Now.AddDays(-1).Date;

            

            return View(await _context.Caja.Where(f=>f.Fecha.Date==fechaAyer || f.Fecha.Date==fechaHoy).ToListAsync());
        }

        // GET: Cajas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var caja = await _context.Caja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caja == null)
            {
                return NotFound();
            }

            return View(caja);
        }

        // GET: Cajas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,diezc,veintec,cincuentac,unb,dosb,cincob,diezb,veinteb,cincuentab,cienb,doscientosb")] Caja caja)
        {
            if (ModelState.IsValid)
            {
                caja.Total = caja.diezc * 0.1+caja.veintec*0.2+caja.cincuentac*0.5+caja.unb*1+caja.dosb*2+caja.cincob*5+caja.diezb*10+caja.veinteb*20+caja.cincuentab*50+caja.cienb*100+caja.doscientosb*200;

                _context.Add(caja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caja);
        }

        // GET: Cajas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caja = await _context.Caja.FindAsync(id);
            if (caja == null)
            {
                return NotFound();
            }
            return View(caja);
        }

        // POST: Cajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,diezc,veintec,cincuentac,unb,dosb,cincob,diezb,veinteb,cincuentab,cienb,doscientosb")] Caja caja)
        {
            if (id != caja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajaExists(caja.Id))
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
            return View(caja);
        }

        // GET: Cajas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caja = await _context.Caja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caja == null)
            {
                return NotFound();
            }

            return View(caja);
        }

        // POST: Cajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caja = await _context.Caja.FindAsync(id);
            _context.Caja.Remove(caja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CajaExists(int id)
        {
            return _context.Caja.Any(e => e.Id == id);
        }
    }
}
