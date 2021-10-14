using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp_nt1.Database;
using tp_nt1.Models;

namespace tp_nt1a_4.Controllers
{
    public class EvolucionesController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public EvolucionesController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Evoluciones
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.Evoluciones.Include(e => e.Episodio).Include(e => e.Nota).Include(e => e.Profesional);
            return View(await historiaClinicaDbContext.ToListAsync());
        }

        // GET: Evoluciones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Episodio)
                .Include(e => e.Nota)
                .Include(e => e.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // GET: Evoluciones/Create
        public IActionResult Create()
        {
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion");
            ViewData["NotaoId"] = new SelectList(_context.Notas, "Id", "Mensaje");
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido");
            return View();
        }

        // POST: Evoluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion,EstadoAbierto,NotaoId,EpisodioId,ProfesionalId")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                evolucion.Id = Guid.NewGuid();
                evolucion.FechaYHoraInicio = DateTime.Now;
                _context.Add(evolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", evolucion.EpisodioId);
            ViewData["NotaoId"] = new SelectList(_context.Notas, "Id", "Mensaje", evolucion.NotaoId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", evolucion.ProfesionalId);
            return View(evolucion);
        }

        // GET: Evoluciones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones.FindAsync(id);
            if (evolucion == null)
            {
                return NotFound();
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", evolucion.EpisodioId);
            ViewData["NotaoId"] = new SelectList(_context.Notas, "Id", "Mensaje", evolucion.NotaoId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", evolucion.ProfesionalId);
            return View(evolucion);
        }

        // POST: Evoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion,EstadoAbierto,NotaoId,EpisodioId,ProfesionalId")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvolucionExists(evolucion.Id))
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
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", evolucion.EpisodioId);
            ViewData["NotaoId"] = new SelectList(_context.Notas, "Id", "Mensaje", evolucion.NotaoId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", evolucion.ProfesionalId);
            return View(evolucion);
        }

        // GET: Evoluciones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Episodio)
                .Include(e => e.Nota)
                .Include(e => e.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // POST: Evoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var evolucion = await _context.Evoluciones.FindAsync(id);
            _context.Evoluciones.Remove(evolucion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvolucionExists(Guid id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }
    }
}
