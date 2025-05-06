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
    public class HistorialReparacionesController : Controller
    {
        private readonly TicketsContext _context;

        public HistorialReparacionesController(TicketsContext context)
        {
            _context = context;
        }

        // GET: HistorialReparaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.HistorialReparaciones.ToListAsync());
        }

        // GET: HistorialReparaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialReparaciones = await _context.HistorialReparaciones
                .FirstOrDefaultAsync(m => m.ReparacionID == id);
            if (historialReparaciones == null)
            {
                return NotFound();
            }

            return View(historialReparaciones);
        }

        // GET: HistorialReparaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistorialReparaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReparacionID,ActivoID,Area,FechaReparacion,DescripcionProblema,TrabajoRealizado,Costo,ProveedorID,Responsable")] HistorialReparaciones historialReparaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialReparaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historialReparaciones);
        }

        // GET: HistorialReparaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialReparaciones = await _context.HistorialReparaciones.FindAsync(id);
            if (historialReparaciones == null)
            {
                return NotFound();
            }
            return View(historialReparaciones);
        }

        // POST: HistorialReparaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReparacionID,ActivoID,Area,FechaReparacion,DescripcionProblema,TrabajoRealizado,Costo,ProveedorID,Responsable")] HistorialReparaciones historialReparaciones)
        {
            if (id != historialReparaciones.ReparacionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialReparaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialReparacionesExists(historialReparaciones.ReparacionID))
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
            return View(historialReparaciones);
        }

        // GET: HistorialReparaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialReparaciones = await _context.HistorialReparaciones
                .FirstOrDefaultAsync(m => m.ReparacionID == id);
            if (historialReparaciones == null)
            {
                return NotFound();
            }

            return View(historialReparaciones);
        }

        // POST: HistorialReparaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialReparaciones = await _context.HistorialReparaciones.FindAsync(id);
            if (historialReparaciones != null)
            {
                _context.HistorialReparaciones.Remove(historialReparaciones);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialReparacionesExists(int id)
        {
            return _context.HistorialReparaciones.Any(e => e.ReparacionID == id);
        }
    }
}
