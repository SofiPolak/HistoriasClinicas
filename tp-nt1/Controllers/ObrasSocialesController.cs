using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = nameof(Rol.Empleado))]
    public class ObrasSocialesController : Controller
    {
        private readonly HistoriaClinicaDbContext _context;

        public ObrasSocialesController(HistoriaClinicaDbContext context)
        {
            _context = context;
        }

        // GET: ObrasSociales
        public async Task<IActionResult> Index()
        {
            return View(await _context.ObrasSociales.ToListAsync());
        }

        // GET: ObrasSociales/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obraSocial = await _context.ObrasSociales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obraSocial == null)
            {
                return NotFound();
            }

            return View(obraSocial);
        }

        // GET: ObrasSociales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ObrasSociales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Activo")] ObraSocial obraSocial)
        {
            if (ModelState.IsValid)
            {
                obraSocial.Id = Guid.NewGuid();
                _context.Add(obraSocial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obraSocial);
        }

        // GET: ObrasSociales/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obraSocial = await _context.ObrasSociales.FindAsync(id);
            if (obraSocial == null)
            {
                return NotFound();
            }
            return View(obraSocial);
        }

        // POST: ObrasSociales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Activo")] ObraSocial obraSocial)
        {
            if (id != obraSocial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obraSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObraSocialExists(obraSocial.Id))
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
            return View(obraSocial);
        }
        /*
        // GET: ObrasSociales/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obraSocial = await _context.ObrasSociales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obraSocial == null)
            {
                return NotFound();
            }

            return View(obraSocial);
        }

        // POST: ObrasSociales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var obraSocial = await _context.ObrasSociales.FindAsync(id);
            _context.ObrasSociales.Remove(obraSocial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool ObraSocialExists(Guid id)
        {
            return _context.ObrasSociales.Any(e => e.Id == id);
        }
    }
}
