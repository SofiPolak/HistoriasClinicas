using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp_nt1.Database;
using tp_nt1.Extensions;
using tp_nt1.Models;
using tp_nt1.Models.Enums;

namespace tp_nt1a_4.Controllers
{
    [Authorize]
    public class ProfesionalesController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public ProfesionalesController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Profesionales
        [Authorize(Roles = nameof(Rol.Empleado))]
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.Profesionales.Include(p => p.Especialidad);
            return View(await historiaClinicaDbContext.ToListAsync());
        }

        // GET: Profesionales/Details/5
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .Include(p => p.Especialidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // GET: Profesionales/Create
        [Authorize(Roles = nameof(Rol.Empleado))]
        public IActionResult Create()
        {
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre");
            return View();
        }

        // POST: Profesionales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Empleado))]
        public async Task<IActionResult> Create(Profesional profesional, string pass)
        {
            try
            {
                pass.ValidarPassword();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(Profesional.Password), ex.Message);
            }

            if (_context.Pacientes.Any(prof => prof.NombreUsuario == profesional.NombreUsuario))
            {
                ModelState.AddModelError(nameof(Profesional.NombreUsuario), "El nombre de usuario ya se encuentra utilizado");
            }

            if (ModelState.IsValid)
            {
                profesional.Id = Guid.NewGuid();
                profesional.FechaAlta = DateTime.Now;
                profesional.Password = pass.Encriptar();
                _context.Add(profesional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre", profesional.EspecialidadId);
            return View(profesional);
        }

        // GET: Profesionales/Edit/5
        [Authorize(Roles = "Empleado,Profesional")]
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
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre", profesional.EspecialidadId);
            return View(profesional);
        }

        // POST: Profesionales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Edit(Guid id, Profesional profesional, string pass)
        {
            if (!string.IsNullOrWhiteSpace(pass))
            {
                try
                {
                    pass.ValidarPassword();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(Profesional.Password), ex.Message);
                }
            }

            if (id != profesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesional);
                    if (!string.IsNullOrWhiteSpace(pass))
                    {
                        profesional.Password = pass.Encriptar();
                    }
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
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre", profesional.EspecialidadId);
            return View(profesional);
        }

        // GET: Profesionales/Delete/5
        [Authorize(Roles = nameof(Rol.Empleado))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesionales
                .Include(p => p.Especialidad)
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
        [Authorize(Roles = nameof(Rol.Empleado))]
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
