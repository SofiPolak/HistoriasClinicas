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
    public class EpicrisisController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public EpicrisisController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Epicrisis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .Include(e => e.Episodio)
                .Include(e => e.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // GET: Epicrisis/Create
        [Authorize(Roles = nameof(Rol.Profesional))]
        public IActionResult Create()
        {
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion");
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido");
            return View();
        }

        // POST: Epicrisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> Create(Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {
                epicrisis.Id = Guid.NewGuid();
                epicrisis.FechaYHora = DateTime.Now;
                epicrisis.ProfesionalId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _context.Add(epicrisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            ViewData["ProfesionalId"] = new SelectList(_context.Profesionales, "Id", "Apellido", epicrisis.ProfesionalId);
            return View(epicrisis);
        }
      
        private bool EpicrisisExists(Guid id)
        {
            return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
