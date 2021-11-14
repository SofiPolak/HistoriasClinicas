using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using tp_nt1.Database;
using tp_nt1.Models;

namespace tp_nt1a_4.Controllers
{
    [Authorize]
    public class EpisodiosController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public EpisodiosController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Episodios
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.Episodios.Include(e => e.EmpleadoRegistra).Include(e => e.HistoriaClinica);
            return View(await historiaClinicaDbContext.ToListAsync());
        }

        // GET: Episodios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .Include(e => e.EmpleadoRegistra)
                .Include(e => e.HistoriaClinica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // GET: Episodios/Create
        [Authorize(Roles = "Empleado,Profesional")]
        public IActionResult Create(Guid hClinicaId)
        {

            ViewBag.hClinicaId = hClinicaId;
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Create(Episodio episodio, Guid hClinicaId)
        {
            if (ModelState.IsValid)
            {
                episodio.EstadoAbierto = true;
                episodio.Id = Guid.NewGuid();
                episodio.FechaYHoraInicio = DateTime.Now;
                episodio.HistoriaId = hClinicaId;
                episodio.EmpleadoId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _context.Add(episodio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(episodio);
        }

        // GET: Episodios/Edit/5
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var episodio = await _context.Episodios.FindAsync(id);
            if (episodio == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoId);
            ViewData["HistoriaId"] = new SelectList(_context.HistoriasClinicas, "Id", "Id", episodio.HistoriaId);
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Edit(Guid id, Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoId);
            ViewData["HistoriaId"] = new SelectList(_context.HistoriasClinicas, "Id", "Id", episodio.HistoriaId);
            return View(episodio);
        }
        /*
        // GET: Episodios/Delete/5
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .Include(e => e.EmpleadoRegistra)
                .Include(e => e.HistoriaClinica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var episodio = await _context.Episodios.FindAsync(id);
            _context.Episodios.Remove(episodio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        private bool EpisodioExists(Guid id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }


        public async Task<IActionResult> MiEpisodio(Guid id)
        {
            var episodio = await _context.Episodios
                  .Include(e => e.EmpleadoRegistra)
                  .Include(e => e.HistoriaClinica)
                  .Include(e => e.RegistroEvoluciones)
                  .FirstOrDefaultAsync(m => m.Id == id);

            return View(episodio);
        }
        /*
        [HttpGet]
        public async Task<IActionResult> CerrarEpisodio(Guid episodioId)
        {
            var episodio = await _context.Episodios.FindAsync(episodioId);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> CerrarEpisodio(Episodio episodio)
        {
            var episodioDb = _context.Episodios.Find(episodio.Id);
            try
            {
                episodioDb.FechaYHoraAlta = episodio.FechaYHoraAlta;
                episodioDb.EstadoAbierto = false;
                episodioDb.FechaYHoraCierre = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodioExists(episodio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("MiHistoriaClinica", "HistoriasClinicas", new { id = episodioDb.HistoriaId });
        }
        */


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public IActionResult PreCerrarEpisodio(Guid id)
        {
            var episodioDb = _context.Episodios
                .Include(episodio => episodio.RegistroEvoluciones)
                .FirstOrDefault(episodio => episodio.Id == id);

            if (episodioDb.EstadoAbierto)
            {
                if (episodioDb.RegistroEvoluciones.Any(evolucion => evolucion.EstadoAbierto))
                {
                    ViewBag.Error = "Hay evoluciones abiertas en este episodio";
                    return RedirectToAction("MiEpisodio", id);
                }
                ViewBag.EpisodioId = id;
                return RedirectToAction("Create", "Diagnosticos");
            }
            return View();

        }
    }
}

