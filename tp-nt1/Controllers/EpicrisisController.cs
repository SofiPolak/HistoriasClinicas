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
                .Include(e => e.Diagnostico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }
            var diagnosticoId = epicrisis.Diagnostico.Id;
            TempData["diagnosticoId"] = diagnosticoId;
            return View(epicrisis);
        }
    }
}
