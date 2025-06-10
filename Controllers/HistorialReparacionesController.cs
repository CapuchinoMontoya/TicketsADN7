using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace TicketsADN7.Controllers
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

            var contexto = _context.HistorialReparaciones
            .Include(r => r.Proveedor);

            return View(await contexto.ToListAsync());
            // return View(await _context.HistorialReparaciones.ToListAsync());
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



        // GET
        public IActionResult Create()
        {
            ViewData["ProveedorID"] = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor");
            ViewData["EquipoID"] = new SelectList(_context.Equipo, "EquipoID", "Nombre");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HistorialReparacionesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var HRep = new HistorialReparaciones
                {
                    ReparacionID = model.ReparacionID,
                    EquipoID = model.EquipoID,
                    Area = model.Area,
                    FechaReparacion = model.FechaReparacion,
                    DescripcionProblema = model.DescripcionProblema,
                    TrabajoRealizado = model.TrabajoRealizado,
                    Costo = model.Costo,
                    ProveedorID = model.ProveedorID,
                    Responsable = model.Responsable
                };
                _context.Add(HRep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProveedorID"] = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", model.ProveedorID);
            ViewData["EquipoID"] = new SelectList(_context.Equipo, "EquipoID", "Nombre", model.EquipoID);
            return View(model);
        }



        // GET: HistorialReparaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reparacion = await _context.HistorialReparaciones.FindAsync(id);
            if (reparacion == null)
                return NotFound();

            // Mapear entidad a ViewModel
            var viewModel = new HistorialReparacionesEditViewModel
            {
                ReparacionID = reparacion.ReparacionID,
                EquipoID = reparacion.EquipoID,
                Area = reparacion.Area,
                FechaReparacion = reparacion.FechaReparacion,
                DescripcionProblema = reparacion.DescripcionProblema,
                TrabajoRealizado = reparacion.TrabajoRealizado,
                Costo = reparacion.Costo,
                ProveedorID = reparacion.ProveedorID,
                Responsable = reparacion.Responsable
            };

            // Preparar lista de proveedores para el dropdown
            ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", viewModel.ProveedorID);
            ViewBag.EquipoID = new SelectList(_context.Equipo, "EquipoID", "Nombre", viewModel.EquipoID);


            return View(viewModel);
        }

        // POST: HistorialReparaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HistorialReparacionesEditViewModel model)
        {
            if (id != model.ReparacionID)
                return NotFound();

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, recargar proveedores para el dropdown
                ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", model.ProveedorID);
                ViewBag.EquipoID = new SelectList(_context.Equipo, "EquipoID", "Equipo", model.EquipoID);
                return View(model);
            }

            try
            {
                // Obtener la entidad original
                var reparacion = await _context.HistorialReparaciones.FindAsync(id);
                if (reparacion == null)
                    return NotFound();

                // Mapear los valores actualizados del ViewModel a la entidad
                reparacion.EquipoID = model.EquipoID;
                reparacion.Area = model.Area;
                reparacion.FechaReparacion = model.FechaReparacion;
                reparacion.DescripcionProblema = model.DescripcionProblema;
                reparacion.TrabajoRealizado = model.TrabajoRealizado;
                reparacion.Costo = model.Costo;
                reparacion.ProveedorID = model.ProveedorID;
                reparacion.Responsable = model.Responsable;

                // Guardar cambios
                _context.Update(reparacion);
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




        // GET: HistorialReparaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialReparaciones = await _context.HistorialReparaciones
                  .Include(r => r.Proveedor)
                     .Include(r => r.Equipo) 
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
