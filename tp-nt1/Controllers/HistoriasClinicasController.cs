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

        [Authorize(Roles = nameof(Rol.Paciente))]
        public IActionResult MiHistoriaClinica()
        {
            var pacienteId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var historiaClinica = _context.HistoriasClinicas
                .Include(historiaClinica => historiaClinica.Episodios)
                .FirstOrDefault(historiaClinica => historiaClinica.PacienteId == pacienteId);

            return View(historiaClinica);
        }


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
