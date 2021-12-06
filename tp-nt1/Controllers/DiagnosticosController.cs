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
        [Authorize(Roles = "Empleado,Profesional")]
        public IActionResult Create()
        {
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id");
            TempData["episodioId"] = TempData["episodioId"];
            if (User.IsInRole(nameof(Rol.Empleado)))
            {
                return View("CreateEmpleado");
            }
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
                episodio.FechaYHoraCierre = DateTime.Now;
                episodio.FechaYHoraAlta = DateTime.Now;
                await _context.SaveChangesAsync();         
                return RedirectToAction("Details", "Epicrisis", new { id = epicrisis.Id });
            }
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
            return View(diagnostico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Empleado))]
        public async Task<IActionResult> CreateEmpleado(Diagnostico diagnostico, Guid episodioId)
        {
            if (ModelState.IsValid)
            {
                diagnostico.Id = Guid.NewGuid();

                var epicrisis = new Epicrisis();
                epicrisis.Id = Guid.NewGuid();
                epicrisis.FechaYHora = DateTime.Now;
                epicrisis.Diagnostico = diagnostico;
                epicrisis.EpisodioId = episodioId;
                diagnostico.Epicrisis = epicrisis;
                _context.Add(diagnostico);
                var episodio = _context.Episodios.Find(episodioId);
                episodio.EstadoAbierto = false;
                episodio.FechaYHoraCierre = DateTime.Now;
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Epicrisis", new { id = epicrisis.Id });
            }
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
            return View(diagnostico);
        }
    }
}
