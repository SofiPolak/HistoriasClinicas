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

            return View(nota);
        }
    }
}
