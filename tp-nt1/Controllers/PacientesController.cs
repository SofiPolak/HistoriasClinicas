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
using tp_nt1.Extensions;
using tp_nt1.Models;

namespace tp_nt1a_4.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public PacientesController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Index()
        {
            var historiaClinicaDbContext = _context.Pacientes.Include(p => p.ObraSocial);
            return View(await historiaClinicaDbContext.ToListAsync());
        }


        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> MisPacientes()
        {
            var profesionalId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var historiaClinicaDbContext = _context.Pacientes.Include(p => p.ObraSocial)
                .Where(p => p.HistoriaClinica.Episodios.Any(e => e.Epicrisis.ProfesionalId == profesionalId));
            return View("Index",await historiaClinicaDbContext.ToListAsync());
        }


        // GET: Pacientes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.ObraSocial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["ObraSocialId"] = new SelectList(_context.ObrasSociales, "Id", "Nombre");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Paciente paciente, string pass)
        {
            try
            {
                pass.ValidarPassword();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(Paciente.Password), ex.Message);
            }

            if (_context.Pacientes.Any(p => p.NombreUsuario == paciente.NombreUsuario))
            {
                ModelState.AddModelError(nameof(Paciente.NombreUsuario), "El nombre de usuario ya se encuentra utilizado");
            }

            if (ModelState.IsValid)
            {
                paciente.Id = Guid.NewGuid();
                paciente.FechaAlta = DateTime.Now;
                paciente.Password = pass.Encriptar();
                paciente.HistoriaClinica = new HistoriaClinica() { PacienteId = paciente.Id, Id = Guid.NewGuid() };
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ObraSocialId"] = new SelectList(_context.ObrasSociales, "Id", "Nombre", paciente.ObraSocialId);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        [Authorize(Roles = "Empleado,Paciente")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["ObraSocialId"] = new SelectList(_context.ObrasSociales, "Id", "Nombre", paciente.ObraSocialId);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Paciente")]
        public async Task<IActionResult> Edit(Guid id, Paciente paciente, string pass)
        {
            if (!string.IsNullOrWhiteSpace(pass))
            {
                try
                {
                    pass.ValidarPassword();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(Paciente.Password), ex.Message);
                }
            }

            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    if (!string.IsNullOrWhiteSpace(pass))
                    {
                        paciente.Password = pass.Encriptar();
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            ViewData["ObraSocialId"] = new SelectList(_context.ObrasSociales, "Id", "Nombre", paciente.ObraSocialId);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        [Authorize(Roles = "Empleado,Paciente")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.ObraSocial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Paciente")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(Guid id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
