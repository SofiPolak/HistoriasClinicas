using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp_nt1.Database;
using tp_nt1.Models;
using tp_nt1.Models.Enums;

namespace tp_nt1a_4.Controllers
{
    [Authorize]
    public class EvolucionesController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public EvolucionesController(HistoriaClinicaDbContext context)
        {
            _context = context;
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
                .Include(e => e.Notas)
                .Include(e => e.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // GET: Evoluciones/Create
        [Authorize(Roles = nameof(Rol.Profesional))]
        public IActionResult Create(Guid episodioId)
        {
            ViewBag.episodioId = episodioId;
            return View();
        }

        // POST: Evoluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = nameof(Rol.Profesional))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evolucion evolucion,Guid episodioId)
        {
            if (ModelState.IsValid)
            {
                evolucion.Id = Guid.NewGuid();
                evolucion.FechaYHoraInicio = DateTime.Now;
                evolucion.ProfesionalId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                evolucion.EpisodioId = episodioId;
                evolucion.EstadoAbierto = true;
                _context.Add(evolucion);
                await _context.SaveChangesAsync();
                ViewBag.episodioId = episodioId;

                return RedirectToAction("Details",new { id = evolucion.Id });
            }
          
            return View(evolucion);
        }
        /*
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
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", evolucion.ProfesionalId);
            return View(evolucion);
        }

        // POST: Evoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Evolucion evolucion)
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
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", evolucion.ProfesionalId);
            return View(evolucion);
        }
    */
        private bool EvolucionExists(Guid id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }

        public async Task<IActionResult> MiEvolucion(Guid evolucionId)
        {
            var evolucion = await _context.Evoluciones
                  .Include(e => e.Notas)
                  .FirstOrDefaultAsync(m => m.Id == evolucionId);  
            return View(evolucion);
        }

        // GET: Evoluciones/Edit/5
        public async Task<IActionResult> CerrarEvolucion(Guid evolucionId)
        {
            var evolucion = await _context.Evoluciones.FindAsync(evolucionId);
            if (evolucion == null)
            {
                return NotFound();
            }
     
            return View(evolucion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarEvolucion(Evolucion evolucion)
        {
            var evolucionDb = _context.Evoluciones.Find(evolucion.Id);
            try
                {
                
                    if (evolucionDb.EstadoAbierto)
                    {
                        evolucionDb.FechaYHoraAlta = evolucion.FechaYHoraAlta;
                        evolucionDb.EstadoAbierto = false;
                        evolucionDb.FechaYHoraCierre = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    
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
                return RedirectToAction("MiEpisodio","Episodios", new {id = evolucionDb.EpisodioId});
        }
    }
}
