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
            var contexto = _context.ControlGarantias
            .Include(r => r.Proveedor);

            return View(await contexto.ToListAsync());
            //return View(await _context.ControlGarantias.ToListAsync());
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
            ViewData["ProveedorID"] = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor");
            return View();
        }

        // POST: ControlGarantias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ControlGarantiasEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var controlGarantia = new ControlGarantias
                {
                    EquipoID = model.EquipoID,
                    ProveedorID = model.ProveedorID,
                    FechaCompra = model.FechaCompra,
                    FechaInicioGarantia = model.FechaInicioGarantia,
                    FechaFinGarantia = model.FechaFinGarantia,
                    TipoGarantia = model.TipoGarantia,
                    Detalles = model.Detalles,
                    EstadoGarantia = model.EstadoGarantia
                };

                _context.Add(controlGarantia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProveedorID"] = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor");
            return View(model);
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

            var viewModel = new ControlGarantiasEditViewModel
            {
                GarantiaID = controlGarantias.GarantiaID,
                EquipoID = controlGarantias.EquipoID,
                ProveedorID = controlGarantias.ProveedorID,
                FechaCompra = controlGarantias.FechaCompra,
                FechaInicioGarantia = controlGarantias.FechaInicioGarantia,
                FechaFinGarantia = controlGarantias.FechaFinGarantia,
                TipoGarantia = controlGarantias.TipoGarantia,
                Detalles = controlGarantias.Detalles,
                EstadoGarantia = controlGarantias.EstadoGarantia
            };

            // Preparar lista de proveedores para el dropdown
            ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", viewModel.ProveedorID);
            return View(viewModel);
        }

        // POST: ControlGarantias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ControlGarantiasEditViewModel model)
        {
            if (id != model.GarantiaID)
                return NotFound();

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, recargar proveedores para el dropdown
                ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", model.ProveedorID);
                return View(model);
            }

            try
            {
                // Obtener la entidad original
                var garantia = await _context.ControlGarantias.FindAsync(id);
                if (garantia == null)
                    return NotFound();

                // Mapear los valores actualizados del ViewModel a la entidad
                garantia.EquipoID = model.EquipoID;
                garantia.ProveedorID = model.ProveedorID;
                garantia.FechaCompra = model.FechaCompra;
                garantia.FechaInicioGarantia = model.FechaInicioGarantia;
                garantia.FechaFinGarantia = model.FechaFinGarantia;
                garantia.TipoGarantia = model.TipoGarantia;
                garantia.Detalles = model.Detalles;
                garantia.EstadoGarantia = model.EstadoGarantia;



                // Guardar cambios
                _context.Update(garantia);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.HistorialReparaciones.Any(e => e.ReparacionID == id))
                    return NotFound();
                else
                    throw;
            }
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
