using INTELISIS.APPCORE.EL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsADN7.Models;
using TicketsADN7.Services;

namespace TicketsADN7.Controllers
{
    public class EquipoController : Controller
    {
        private readonly TicketsContext _context;
        private readonly IQrCodeService _qrCodeService;

        public EquipoController(TicketsContext context, IQrCodeService qrCodeService)
        {
            _context = context;
            _qrCodeService = qrCodeService;
        }

        /// <summary>
        /// Muestra la lista de todos los equipos registrados.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var ticketsContext = _context.Equipo.Include(e => e.Departamento).Include(e => e.Usuario);
            return View(await ticketsContext.ToListAsync());
        }

        /// <summary>
        /// Muestra los detalles de un equipo específico.
        /// </summary>
        /// <param name="id">ID del equipo</param>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var equipo = await _context.Equipo
                .Include(e => e.Departamento)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EquipoID == id);
            if (equipo == null)
                return NotFound();

            return View(equipo);
        }

        /// <summary>
        /// Muestra el formulario para crear un nuevo equipo.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento");
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario");
            return View();
        }

        /// <summary>
        /// Procesa el formulario de creación de equipo y genera un código QR.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Marca,Modelo,NumeroSerie,DepartamentoID,UsuarioID")] Equipo equipo)
        {
            try
            {
                ModelState.Remove("EquipoID");
                ModelState.Remove("FechaRegistro");
                ModelState.Remove("Departamento");
                ModelState.Remove("Usuario");
                ModelState.Remove("CodigoQr");

                if (equipo.FechaRegistro == DateTime.MinValue)
                    equipo.FechaRegistro = DateTime.Now;

                if (ModelState.IsValid)
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            equipo.CodigoQr = "";
                            _context.Add(equipo);
                            await _context.SaveChangesAsync();
                            string qrContent = $"EQUIPO ID: {equipo.EquipoID}\nNombre: {equipo.Nombre}\nSerie: {equipo.NumeroSerie}";
                            equipo.CodigoQr = _qrCodeService.GenerateQrCodeBase64(qrContent);

                            _context.Update(equipo);
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();

                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            ModelState.AddModelError("", $"Error generando QR: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento", equipo.DepartamentoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", equipo.UsuarioID);
            return View(equipo);
        }

        /// <summary>
        /// Muestra el formulario para editar un equipo existente.
        /// </summary>
        /// <param name="id">ID del equipo</param>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipo = await _context.Equipo.FindAsync(id);
            if (equipo == null)
                return NotFound();

            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento", equipo.DepartamentoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", equipo.UsuarioID);
            return View(equipo);
        }

        /// <summary>
        /// Procesa la edición del equipo, actualiza su información y regenera el código QR.
        /// </summary>
        /// <param name="id">ID del equipo</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoID,Nombre,Descripcion,Marca,Modelo,NumeroSerie,DepartamentoID,UsuarioID")] Equipo equipo)
        {
            if (id != equipo.EquipoID)
            {
                return NotFound();
            }
            ModelState.Remove("Usuario");
            ModelState.Remove("Departamento");
            if (ModelState.IsValid)
            {
                try
                {
                    string qrContent = $"EQUIPO ID: {equipo.EquipoID}\nNombre: {equipo.Nombre}\nSerie: {equipo.NumeroSerie}";
                    equipo.CodigoQr = _qrCodeService.GenerateQrCodeBase64(qrContent);

                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.EquipoID))
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

            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento", equipo.DepartamentoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", equipo.UsuarioID);

            return View(equipo);
        }

        /// <summary>
        /// Muestra la vista de confirmación para eliminar un equipo.
        /// </summary>
        /// <param name="id">ID del equipo</param>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipo = await _context.Equipo
                .Include(e => e.Departamento)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EquipoID == id);
            if (equipo == null)
                return NotFound();

            return View(equipo);
        }

        /// <summary>
        /// Elimina definitivamente un equipo de la base de datos.
        /// </summary>
        /// <param name="id">ID del equipo</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipo.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipo.Remove(equipo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica si existe un equipo por su ID.
        /// </summary>
        /// <param name="id">ID del equipo</param>
        private bool EquipoExists(int id)
        {
            return _context.Equipo.Any(e => e.EquipoID == id);
        }
    }
}
