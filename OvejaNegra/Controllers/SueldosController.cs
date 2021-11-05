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
    public class SueldosController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHerlper;

        public SueldosController(DataContext context, ICombosHelper combosHerlper)
        {
            _context = context;
            _combosHerlper = combosHerlper;
        }

        // GET: Sueldos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sueldo.Include(e=>e.Empleado).ToListAsync());
        }

        // GET: Sueldos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sueldo = await _context.Sueldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sueldo == null)
            {
                return NotFound();
            }

            return View(sueldo);
        }

        // GET: Sueldos/Create
        public IActionResult Create()
        {
            var model = new SueldoViewModel
            {

                Empleados = _combosHerlper.GetComboEmpleado(),

            };

            return View(model);
        }

        // POST: Sueldos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,HoraE,HoraS,HoraT,Jornal,Bono,Total,Pago,EmpleadoId")] SueldoViewModel model)
        {
            var fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4,0,0));
            var bono = _context.Pedidos.Where(c => c.Fecha.Date == fecha.Date);
            var empleado= _context.Empleado.Find(model.EmpleadoId);

            var sueldo = new Sueldo();

            if (ModelState.IsValid)
            {
                sueldo.HoraE = model.HoraE;
                sueldo.HoraS = model.HoraS;
                sueldo.Fecha = fecha.Date;
                sueldo.HoraT =(model.HoraS-model.HoraE).TotalHours;
                sueldo.Bono = bono.Sum(p => p.BonoT);
                sueldo.Jornal = sueldo.HoraT * empleado.Sueldo;
                sueldo.Total = sueldo.Bono + sueldo.Jornal;
                sueldo.Empleado = empleado;

                _context.Add(sueldo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Sueldos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sueldo = await _context.Sueldo.FindAsync(id);
            if (sueldo == null)
            {
                return NotFound();
            }
            return View(sueldo);
        }

        // POST: Sueldos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,HoraE,HoraS,HoraT,Jornal,Bono,Total,Pago")] Sueldo sueldo)
        {
            if (id != sueldo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sueldo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SueldoExists(sueldo.Id))
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
            return View(sueldo);
        }

        // GET: Sueldos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sueldo = await _context.Sueldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sueldo == null)
            {
                return NotFound();
            }

            return View(sueldo);
        }

        // POST: Sueldos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sueldo = await _context.Sueldo.FindAsync(id);
            _context.Sueldo.Remove(sueldo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SueldoExists(int id)
        {
            return _context.Sueldo.Any(e => e.Id == id);
        }
    }
}
