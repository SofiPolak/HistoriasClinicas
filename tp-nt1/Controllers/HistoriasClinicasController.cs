using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp_nt1.Database;
using tp_nt1.Models;

namespace tp_nt1a_4.Controllers
{
    [Authorize]
    public class HistoriasClinicasController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public HistoriasClinicasController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: HistoriasClinicas
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.HistoriasClinicas.Include(h => h.Paciente);
            return View(await historiaClinicaDbContext.ToListAsync());
        }

        // GET: HistoriasClinicas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriasClinicas
                .Include(h => h.Paciente)              
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // GET: HistoriasClinicas/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido");
            return View();
        }

        // POST: HistoriasClinicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {
            if (ModelState.IsValid)
            {
                historiaClinica.Id = Guid.NewGuid();
                _context.Add(historiaClinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", historiaClinica.PacienteId);
            return View(historiaClinica);
        }

        // GET: HistoriasClinicas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriasClinicas.FindAsync(id);
            if (historiaClinica == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", historiaClinica.PacienteId);
            return View(historiaClinica);
        }

        // POST: HistoriasClinicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historiaClinica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriaClinicaExists(historiaClinica.Id))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", historiaClinica.PacienteId);
            return View(historiaClinica);
        }

        // GET: HistoriasClinicas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriasClinicas
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // POST: HistoriasClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var historiaClinica = await _context.HistoriasClinicas.FindAsync(id);
            _context.HistoriasClinicas.Remove(historiaClinica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaClinicaExists(Guid id)
        {
            return _context.HistoriasClinicas.Any(e => e.Id == id);
        }

      
    }
}
