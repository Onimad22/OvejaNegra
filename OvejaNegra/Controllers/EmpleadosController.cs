using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OvejaNegra.Data;
using OvejaNegra.Data.Entities;
using OvejaNegra.Helpers;
using OvejaNegra.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OvejaNegra.Controllers
{
    [Authorize(Roles = "Admin")]
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
                .Include(s => s.Sueldos.Where(p => p.Pago == false))
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


                EmpleadoId = empleado.Id,

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

        // GET: Sueldos/Edit/5
        public IActionResult SueldoEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sueldo = _context.Sueldo
                .Include(e => e.Empleado)
                .Where(s => s.Id == id)
                //.ToList()
                //.Find(id.Value)
                .FirstOrDefault()
                ;

            var sueldoModel = new SueldoViewModel
            {

                Fecha = sueldo.Fecha,
                HoraE = sueldo.HoraE,
                HoraS = sueldo.HoraS,
                EmpleadoId = sueldo.Empleado.Id,
                Id = sueldo.Id
            };

            if (sueldo == null)
            {
                return NotFound();
            }


            return View(sueldoModel);
        }

        // POST: Sueldos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SueldoEdit(int id, [Bind("Id,Fecha,HoraE,HoraS,EmpleadoId")] SueldoViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }


            var bono = _context.Pedidos.Where(c => c.Fecha.Date == model.Fecha.Date);
            var empleado = _context.Empleado.Find(model.EmpleadoId);

            var sueldo = new Sueldo();


            if (ModelState.IsValid)
            {
                try
                {
                    sueldo.Id = model.Id;
                    sueldo.Fecha = model.Fecha;
                    sueldo.HoraE = model.HoraE;
                    sueldo.HoraS = model.HoraS;
                    sueldo.HoraT = (model.HoraS - model.HoraE).TotalHours;
                    sueldo.Bono = bono.Sum(p => p.BonoT);
                    sueldo.Jornal = sueldo.HoraT * empleado.Sueldo;
                    sueldo.Total = sueldo.Bono + sueldo.Jornal;
                    sueldo.Empleado = empleado;


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


        private bool SueldoExists(int id)
        {
            return _context.Sueldo.Any(e => e.Id == id);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> DeleteSueldo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Sueldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("DeleteSueldo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSueldoConfirmed(int id)
        {
            var empleado = await _context.Sueldo.FindAsync(id);
            _context.Sueldo.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: PAGAR SUELDOS
        public async Task<IActionResult> PagarSueldo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sueldo = _context.Sueldo
                .Include(s => s.Empleado)
                .Where(m => m.Empleado.Id == id)
                .Where(p => p.Pago == false).ToList();
            if (sueldo == null)
            {
                return NotFound();
            }


            foreach (var item in sueldo)
            {

                item.Pago = true;


                _context.Update(item);
                await _context.SaveChangesAsync();

            }

            var fecha = DateTimeOffset.Now.ToOffset(new TimeSpan(-4, 0, 0));
            var sueldoPago = new SueldoPago();

            sueldoPago.Fecha = fecha;
            sueldoPago.Total = sueldo.Sum(s => s.Total);
            sueldoPago.Empleado = _context.Empleado.Find(id);

            _context.SueldosPago.Add(sueldoPago);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Empleados", new { id = id });
        }

        // GET: SueldosPago
        public async Task<IActionResult> VerSueldos()
        {
            return View(await _context.SueldosPago.Include(e => e.Empleado).ToListAsync());
        }

    }
}
