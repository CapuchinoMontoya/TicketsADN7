using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;

namespace TicketsADN7.Views
{
    public class ControlGarantiasController : Controller
    {
        private readonly TicketsContext _context;

        public ControlGarantiasController(TicketsContext context)
        {
            _context = context;
        }

        // GET: ControlGarantias
        public async Task<IActionResult> Index()
        {
            return View(await _context.ControlGarantias.ToListAsync());
        }

        // GET: ControlGarantias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGarantias = await _context.ControlGarantias
                .FirstOrDefaultAsync(m => m.GarantiaID == id);
            if (controlGarantias == null)
            {
                return NotFound();
            }

            return View(controlGarantias);
        }

        // GET: ControlGarantias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControlGarantias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GarantiaID,ActivoID,ProveedorID,FechaInicioGarantia,FechaFinGarantia,TipoGarantia,Detalles,EstadoGarantia")] ControlGarantias controlGarantias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlGarantias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(controlGarantias);
        }

        // GET: ControlGarantias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGarantias = await _context.ControlGarantias.FindAsync(id);
            if (controlGarantias == null)
            {
                return NotFound();
            }
            return View(controlGarantias);
        }

        // POST: ControlGarantias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GarantiaID,ActivoID,ProveedorID,FechaInicioGarantia,FechaFinGarantia,TipoGarantia,Detalles,EstadoGarantia")] ControlGarantias controlGarantias)
        {
            if (id != controlGarantias.GarantiaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlGarantias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlGarantiasExists(controlGarantias.GarantiaID))
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
            return View(controlGarantias);
        }

        // GET: ControlGarantias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlGarantias = await _context.ControlGarantias
                .FirstOrDefaultAsync(m => m.GarantiaID == id);
            if (controlGarantias == null)
            {
                return NotFound();
            }

            return View(controlGarantias);
        }

        // POST: ControlGarantias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlGarantias = await _context.ControlGarantias.FindAsync(id);
            if (controlGarantias != null)
            {
                _context.ControlGarantias.Remove(controlGarantias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlGarantiasExists(int id)
        {
            return _context.ControlGarantias.Any(e => e.GarantiaID == id);
        }
    }
}
