using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


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
        public async Task<IActionResult> Create([Bind("ReparacionID,EquipoID,Area,FechaReparacion,DescripcionProblema,TrabajoRealizado,Costo,ProveedorID,Responsable")] HistorialReparaciones historialReparaciones)
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
        /*public async Task<IActionResult> Edit(int? id)
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
        }*/
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var historial = await _context.HistorialReparaciones.FindAsync(id);
            if (historial == null) return NotFound();

            ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", historial.ProveedorID);
            return View(historial);
        }*/
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

            return View(viewModel);
        }


        // POST: HistorialReparaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReparacionID,EquipoID,Area,FechaReparacion,DescripcionProblema,TrabajoRealizado,Costo,ProveedorID,Responsable")] HistorialReparaciones historialReparaciones)
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
        }*/
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, HistorialReparaciones historialReparaciones)
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
                     return RedirectToAction(nameof(Index));
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
             }

             // Volver a cargar el ViewBag si hay error para que el dropdown funcione
             ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", historialReparaciones.ProveedorID);

             return View(historialReparaciones);
         }*/

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HistorialReparaciones historialReparaciones)
        {
            if (id != historialReparaciones.ReparacionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var registroExistente = await _context.HistorialReparaciones.FindAsync(id);
                    if (registroExistente == null)
                    {
                        return NotFound();
                    }

                    // Asignamos los valores actualizados
                    registroExistente.EquipoID = historialReparaciones.EquipoID;
                    registroExistente.Area = historialReparaciones.Area;
                    registroExistente.FechaReparacion = historialReparaciones.FechaReparacion;
                    registroExistente.DescripcionProblema = historialReparaciones.DescripcionProblema;
                    registroExistente.TrabajoRealizado = historialReparaciones.TrabajoRealizado;
                    registroExistente.Costo = historialReparaciones.Costo;
                    registroExistente.ProveedorID = historialReparaciones.ProveedorID;
                    registroExistente.Responsable = historialReparaciones.Responsable;

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
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
            }

            // Recargar dropdown si hay error
            ViewBag.ProveedorID = new SelectList(_context.CatalogoProveedores, "ProveedorID", "NombreProveedor", historialReparaciones.ProveedorID);
            return View(historialReparaciones);
        }

        */
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
