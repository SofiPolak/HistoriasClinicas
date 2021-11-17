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
    public class HistoriasClinicasController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public HistoriasClinicasController(HistoriaClinicaDbContext context)
        {
            _context = context;
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
        private bool HistoriaClinicaExists(Guid id)
        {
            return _context.HistoriasClinicas.Any(e => e.Id == id);
        }

        // metodo que consigue la historia clinica del paciente loggeado
        [Authorize(Roles = nameof(Rol.Paciente))]
        public IActionResult MiHistoriaClinica()
        {
            var pacienteId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var historiaClinica = _context.HistoriasClinicas
                .Include(historiaClinica => historiaClinica.Episodios)
                .FirstOrDefault(historiaClinica => historiaClinica.PacienteId == pacienteId);
                

            return View(historiaClinica);
        }

        // metodo que consigue la historia clinica de un paciente
        [Authorize(Roles = "Empleado, Profesional")]
        public IActionResult UnaHistoriaClinica(Guid pacienteId)
        {

            var historiaClinica = _context.HistoriasClinicas
                .Include(historiaClinica => historiaClinica.Episodios)
                .FirstOrDefault(historiaClinica => historiaClinica.PacienteId == pacienteId);

            TempData["pacienteId"] = pacienteId;
            return View("MiHistoriaClinica", historiaClinica);
        }

    
    }
}
