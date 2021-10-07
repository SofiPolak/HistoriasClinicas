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
    public class ProfesionalesController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public ProfesionalesController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Profesionales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesionales.ToListAsync());
        }

        // GET: Profesionales/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // GET: Profesionales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesionales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,Especialidad,Id,NombreUsuario,Nombre,Apellido,Email,FechaAlta,DNI,Telefono,Direccion")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                profesional.Id = Guid.NewGuid();
                _context.Add(profesional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesional);
        }

        // GET: Profesionales/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }
            return View(profesional);
        }

        // POST: Profesionales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Matricula,Especialidad,Id,NombreUsuario,Nombre,Apellido,Email,FechaAlta,DNI,Telefono,Direccion")] Profesional profesional)
        {
            if (id != profesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionalExists(profesional.Id))
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
            return View(profesional);
        }

        // GET: Profesionales/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // POST: Profesionales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);
            _context.Profesionales.Remove(profesional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionalExists(Guid id)
        {
            return _context.Profesionales.Any(e => e.Id == id);
        }
    }
}
