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
    public class DiagnosticosController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public DiagnosticosController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Diagnosticos
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.Diagnosticos.Include(d => d.Epicrisis);
            return View(await historiaClinicaDbContext.ToListAsync());
        }

        // GET: Diagnosticos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Epicrisis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticos/Create
        [Authorize(Roles = nameof(Rol.Profesional))]
        public IActionResult Create()
        {
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id");
            ViewData["episodioId"] = TempData["episodioId"];
                
            return View();
        }

        // POST: Diagnosticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> Create(Diagnostico diagnostico, Guid episodioId)
        {
            if (ModelState.IsValid)
            {
                diagnostico.Id = Guid.NewGuid();

                var epicrisis = new Epicrisis();
                epicrisis.Id = Guid.NewGuid();
                epicrisis.FechaYHora = DateTime.Now;
                epicrisis.Diagnostico = diagnostico;
                epicrisis.ProfesionalId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                epicrisis.EpisodioId = episodioId;
                diagnostico.Epicrisis = epicrisis;
                _context.Add(diagnostico);
                var episodio = _context.Episodios.Find(episodioId);
                episodio.EstadoAbierto = false;
                await _context.SaveChangesAsync(); 
                return RedirectToAction("Details", "Epicrisis", new { id = epicrisis.Id });
            }
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
            return View(diagnostico);
        }

        // GET: Diagnosticos/Edit/5
        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
            return View(diagnostico);
        }

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descripcion,Recomendacion,EpicrisisId")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Id))
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
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
            return View(diagnostico);
        }

        /*
        // GET: Diagnosticos/Delete/5
        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Epicrisis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            _context.Diagnosticos.Remove(diagnostico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        private bool DiagnosticoExists(Guid id)
        {
            return _context.Diagnosticos.Any(e => e.Id == id);
        }
    }
}
