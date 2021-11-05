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
    public class EmpleadosController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHerlper;

        public EmpleadosController(DataContext context, ICombosHelper combosHerlper)
        {
            _context = context;
            _combosHerlper = combosHerlper;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleado.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(s=>s.Sueldos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Sueldo")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Sueldo")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.Id == id);
        }

        // GET: Sueldos/Create
        public async Task<IActionResult> AddSueldo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id.Value);
            if (empleado == null)
            {
                return NotFound();
            }

            var model = new SueldoViewModel
            {

                
                EmpleadoId=empleado.Id,

            };

            return View(model);
        }

        // POST: Sueldos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSueldo(SueldoViewModel model)
        {
            var fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0));
            var bono = _context.Pedidos.Where(c => c.Fecha.Date == fecha.Date);
            var empleado = _context.Empleado.Find(model.EmpleadoId);

            var sueldo = new Sueldo();

            if (ModelState.IsValid)
            {
                sueldo.HoraE = model.HoraE;
                sueldo.HoraS = model.HoraS;
                sueldo.Fecha = fecha.Date;
                sueldo.HoraT = (model.HoraS - model.HoraE).TotalHours;
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

    }
}
