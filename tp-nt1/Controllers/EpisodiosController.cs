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
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido");
            ViewData["HistoriaId"] = new SelectList(_context.HistoriasClinicas, "Id", "Id");
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoId,HistoriaId")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                episodio.Id = Guid.NewGuid();
                _context.Add(episodio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoId);
            ViewData["HistoriaId"] = new SelectList(_context.HistoriasClinicas, "Id", "Id", episodio.HistoriaId);
            return View(episodio);
        }

        // GET: Episodios/Edit/5
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoId,HistoriaId")] Episodio episodio)
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

        // GET: Episodios/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var episodio = await _context.Episodios.FindAsync(id);
            _context.Episodios.Remove(episodio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(Guid id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }
    }
}
