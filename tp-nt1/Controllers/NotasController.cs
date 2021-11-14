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
    public class NotasController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public NotasController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.Notas.Include(n => n.Empleado).Include(n => n.Profesional);
            return View(await historiaClinicaDbContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Empleado)
                .Include(n => n.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }
            var evolucion = _context.Evoluciones.Find(nota.EvolucionId);
            ViewBag.Estado = evolucion.EstadoAbierto;
            return View(nota);
        }

        // GET: Notas/Create
        [Authorize(Roles = "Empleado,Profesional")]
        public IActionResult Create(Guid evolucionId)
        {
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido");
            //ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido");

            ViewBag.evolucionId = evolucionId;
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Create(Guid evolucionId, Nota nota)
        {
            if (ModelState.IsValid)
            {
                nota.Id = Guid.NewGuid();
                nota.FechaYHora = DateTime.Now;
                nota.EvolucionId = evolucionId;
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if(User.IsInRole(nameof(Rol.Empleado)))
                {
                    nota.EmpleadoId = usuarioId;
                }
                else
                {
                    nota.ProfesionalId = usuarioId;
                }
                _context.Add(nota);
                await _context.SaveChangesAsync();
                ViewBag.EvolucionId = evolucionId;
                return RedirectToAction("Details", new { id = nota.Id });
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", nota.EmpleadoId);
            //ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", nota.ProfesionalId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", nota.EmpleadoId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", nota.ProfesionalId);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Mensaje,FechaYHora,EmpleadoId,ProfesionalId")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", nota.EmpleadoId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", nota.ProfesionalId);
            return View(nota);
        }
        /*
        // GET: Notas/Delete/5
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Empleado)
                .Include(n => n.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nota = await _context.Notas.FindAsync(id);
            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool NotaExists(Guid id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}
