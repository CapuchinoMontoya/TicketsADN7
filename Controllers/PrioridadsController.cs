using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;

namespace TicketsADN7.Controllers
{
    public class PrioridadsController : Controller
    {
        private readonly TicketsContext _context;

        public PrioridadsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: Prioridads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prioridad.ToListAsync());
        }

        // GET: Prioridads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prioridad = await _context.Prioridad
                .FirstOrDefaultAsync(m => m.PrioridadID == id);
            if (prioridad == null)
            {
                return NotFound();
            }

            return View(prioridad);
        }

        // GET: Prioridads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prioridads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrioridadID,Nombre,Nivel,Descripcion")] Prioridad prioridad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prioridad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prioridad);
        }

        // GET: Prioridads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prioridad = await _context.Prioridad.FindAsync(id);
            if (prioridad == null)
            {
                return NotFound();
            }
            return View(prioridad);
        }

        // POST: Prioridads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrioridadID,Nombre,Nivel,Descripcion")] Prioridad prioridad)
        {
            if (id != prioridad.PrioridadID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prioridad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrioridadExists(prioridad.PrioridadID))
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
            return View(prioridad);
        }

        // GET: Prioridads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prioridad = await _context.Prioridad
                .FirstOrDefaultAsync(m => m.PrioridadID == id);
            if (prioridad == null)
            {
                return NotFound();
            }

            return View(prioridad);
        }

        // POST: Prioridads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prioridad = await _context.Prioridad.FindAsync(id);
            if (prioridad != null)
            {
                _context.Prioridad.Remove(prioridad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrioridadExists(int id)
        {
            return _context.Prioridad.Any(e => e.PrioridadID == id);
        }
    }
}
