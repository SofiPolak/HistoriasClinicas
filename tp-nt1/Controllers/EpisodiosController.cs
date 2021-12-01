using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using tp_nt1.Database;
using tp_nt1.Models;

namespace tp_nt1a_4.Controllers
{
    [Authorize]
    public class EpisodiosController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public EpisodiosController(HistoriaClinicaDbContext context)
        {
            _context = context;
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
                .Include(e => e.Epicrisis)
                .FirstOrDefaultAsync(m => m.Id == id);
     
            if (episodio == null)
            {
                return NotFound();
            }

            TempData["pacienteId"] = _context.HistoriasClinicas.Find(episodio.HistoriaId).PacienteId;

            if (!episodio.EstadoAbierto)
            {
                var epicrisis = episodio.Epicrisis;
                var epicrisisId = epicrisis.Id;
                TempData["epicrisisId"] = epicrisisId;
            }
            return View(episodio);
        }

        // GET: Episodios/Create
        [Authorize(Roles = "Empleado")]
        public IActionResult Create(Guid hClinicaId)
        {
            ViewBag.pacienteId = _context.HistoriasClinicas.Find(hClinicaId).PacienteId;
            ViewBag.hClinicaId = hClinicaId;
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Create(Episodio episodio, Guid hClinicaId)
        {
            if (ModelState.IsValid)
            {
                episodio.EstadoAbierto = true;
                episodio.Id = Guid.NewGuid();
                episodio.FechaYHoraInicio = DateTime.Now;
                episodio.HistoriaId = hClinicaId;
                episodio.EmpleadoId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _context.Add(episodio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Pacientes");
            }

            return View(episodio);
        }
        /*
        // GET: Episodios/Edit/5
        [Authorize(Roles = "Empleado,Profesional")]
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
        [Authorize(Roles = "Empleado,Profesional")]
        public async Task<IActionResult> Edit(Guid id, Episodio episodio)
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
        */
        private bool EpisodioExists(Guid id)
        {
            return _context.Episodios.Any(e => e.Id == id);
        }


        public async Task<IActionResult> MiEpisodio(Guid id)
        {
            var episodio = await _context.Episodios
                  .Include(e => e.EmpleadoRegistra)
                  .Include(e => e.HistoriaClinica)
                  .Include(e => e.RegistroEvoluciones)
                  .FirstOrDefaultAsync(m => m.Id == id);
            return View(episodio);
        }

        [HttpPost]
        [Authorize(Roles = "Profesional")]
        public IActionResult PreCerrarEpisodio(Guid id)
        {
            var episodioDb = _context.Episodios
                .Include(episodio => episodio.RegistroEvoluciones)
                .FirstOrDefault(episodio => episodio.Id == id);

            if (episodioDb.EstadoAbierto)
            {
                if (episodioDb.RegistroEvoluciones.Any(evolucion => evolucion.EstadoAbierto))
                {
                    ViewBag.Error = "Hay evoluciones abiertas en este episodio";
                    return RedirectToAction("Details", id);
                }
                TempData["episodioId"] = id;
                return RedirectToAction("Create", "Diagnosticos");
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public IActionResult PreCerrarEpisodioAdministrativo(Guid id)
        {
            var episodioDb = _context.Episodios
                .FirstOrDefault(episodio => episodio.Id == id);

            if (episodioDb.EstadoAbierto && episodioDb.RegistroEvoluciones == null)
            {
                TempData["episodioId"] = id;
                return RedirectToAction("Create", "Diagnosticos");
            }
            return RedirectToAction("Details", new { id });
        }
    }
}

