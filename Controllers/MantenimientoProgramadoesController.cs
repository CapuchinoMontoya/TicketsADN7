using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketsADN7.Controllers
{
    public class MantenimientoProgramadoesController : Controller
    {
        private readonly TicketsContext _context;

        public MantenimientoProgramadoesController(TicketsContext context)
        {
            _context = context;
        }

        // GET: MantenimientoProgramadoes
        public async Task<IActionResult> Index()
        {

            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento");
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Nombre");
            ViewData["ChecklistID"] = new SelectList(_context.Checklist.ToList(), "ChecklistID", "Nombre");
            return View(await _context.MantenimientoProgramado.ToListAsync());
        }

        // GET: MantenimientoProgramadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimientoProgramado = await _context.MantenimientoProgramado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }

            return View(mantenimientoProgramado);
        }

        // GET: MantenimientoProgramadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MantenimientoProgramadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Categoria,Departamento,FrecuenciaDias,FechaUltimaRevision,FechaProximaRevision,Activo")] MantenimientoProgramado mantenimientoProgramado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mantenimientoProgramado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mantenimientoProgramado);
        }

        // GET: MantenimientoProgramadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimientoProgramado = await _context.MantenimientoProgramado.FindAsync(id);
            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }
            return View(mantenimientoProgramado);
        }

        // POST: MantenimientoProgramadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Categoria,Departamento,FrecuenciaDias,FechaUltimaRevision,FechaProximaRevision,Activo")] MantenimientoProgramado mantenimientoProgramado)
        {
            if (id != mantenimientoProgramado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mantenimientoProgramado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MantenimientoProgramadoExists(mantenimientoProgramado.Id))
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
            return View(mantenimientoProgramado);
        }

        // GET: MantenimientoProgramadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimientoProgramado = await _context.MantenimientoProgramado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }

            return View(mantenimientoProgramado);
        }

        // POST: MantenimientoProgramadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mantenimientoProgramado = await _context.MantenimientoProgramado.FindAsync(id);
            if (mantenimientoProgramado != null)
            {
                _context.MantenimientoProgramado.Remove(mantenimientoProgramado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MantenimientoProgramadoExists(int id)
        {
            return _context.MantenimientoProgramado.Any(e => e.Id == id);
        }

        // Obtener eventos para el calendario
        public async Task<IActionResult> GetEventos()
        {
            var eventos = await (
            from m in _context.MantenimientoProgramado
            join c in _context.CategoriaTicket
                on m.Categoria equals c.CategoriaID.ToString()
            where m.Activo
            select new
            {
                title = m.Nombre + " (" + c.Nombre + ")",
                start = m.FechaProximaRevision.ToString("yyyy-MM-dd"),
                allDay = true
            }
        ).ToListAsync();


            return Json(eventos);
        }

        // Crear desde el calendario (Ajax)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDesdeCalendario([FromBody] MantenimientoProgramado mantenimiento)
        {
            if (ModelState.IsValid)
            {
                mantenimiento.Activo = true;

                if (mantenimiento.FrecuenciaDias > 0 && mantenimiento.FechaProximaRevision != null)
                {
                    DateTime fechaActual = mantenimiento.FechaProximaRevision;
                    DateTime fechaLimite = mantenimiento.FechaUltimaRevision.Value;

                    while (fechaActual <= fechaLimite)
                    {
                        if (fechaActual.DayOfWeek != DayOfWeek.Sunday)
                        {
                            var nuevoMantenimiento = new MantenimientoProgramado
                            {
                                Nombre = mantenimiento.Nombre,
                                Categoria = mantenimiento.Categoria,
                                Departamento = mantenimiento.Departamento,
                                ChecklistId = mantenimiento.ChecklistId,
                                FrecuenciaDias = mantenimiento.FrecuenciaDias,
                                FechaProximaRevision = fechaActual,
                                Activo = true
                            };

                            _context.MantenimientoProgramado.Add(nuevoMantenimiento);
                        }
                        else
                        {
                            var nuevoMantenimiento = new MantenimientoProgramado
                            {
                                Nombre = mantenimiento.Nombre,
                                Categoria = mantenimiento.Categoria,
                                Departamento = mantenimiento.Departamento,
                                ChecklistId = mantenimiento.ChecklistId,
                                FrecuenciaDias = mantenimiento.FrecuenciaDias,
                                FechaProximaRevision = fechaActual.AddDays(1),
                                Activo = true
                            };

                            _context.MantenimientoProgramado.Add(nuevoMantenimiento);
                        }
                        fechaActual = fechaActual.AddDays(mantenimiento.FrecuenciaDias);
                    }
                }
                else
                {
                    _context.MantenimientoProgramado.Add(mantenimiento);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        public async Task<IActionResult> RealizarChecklist(int? id)
        {
            if (id == null)
                return NotFound();

            var ticketChecklist = await _context.TicketChecklist
                .Include(tc => tc.Checklist)
                    .ThenInclude(c => c.Campos) 
                .FirstOrDefaultAsync(tc => tc.TicketID == id);

            if (ticketChecklist == null)
                return NotFound();

            return View(ticketChecklist);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarChecklist(int ticketChecklistId)
         {
            var ticketChecklist = await _context.TicketChecklist
                .Include(tc => tc.Checklist)
                    .ThenInclude(c => c.Campos)
                .FirstOrDefaultAsync(tc => tc.TicketChecklistID == ticketChecklistId);

            if (ticketChecklist == null)
                return NotFound();

            foreach (var campo in ticketChecklist.Checklist.Campos)
            {
                string inputName = $"campo_{campo.ChecklistCampoID}";
                string valor = null;

                if (campo.Tipo == TipoCampo.Checkbox) // Checkbox
                {
                    valor = Request.Form[inputName].FirstOrDefault() == "on" ? "true" : "false";
                }
                else if (campo.Tipo == TipoCampo.Archivo)
                {
                    var archivo = Request.Form.Files[inputName];
                    if (archivo != null && archivo.Length > 0)
                    {
                        var archivoUpload = await GuardarArchivo(archivo);

                        valor = archivoUpload != "Error" ? archivoUpload : "Error";
                    }
                }
                else
                {
                    valor = Request.Form[inputName].FirstOrDefault();
                }

                // Validar si es requerido y no se llenó
                if (campo.RequiereEvidencia && string.IsNullOrWhiteSpace(valor))
                {
                    TempData["ToastrType"] = "warning";
                    TempData["ToastrMessage"] = $"El campo '{campo.NombreCampo}' es obligatorio.";
                    return RedirectToAction("RealizarChecklist", new { id = ticketChecklist.TicketID });
                }

                var respuesta = new RespuestaChecklistCampo
                {
                    TicketChecklistID = ticketChecklist.TicketChecklistID,
                    ChecklistCampoID = campo.ChecklistCampoID,
                    Valor = valor
                };

                _context.RespuestaChecklistCampo.Add(respuesta);
            }

            await _context.SaveChangesAsync();

            TempData["ToastrType"] = "success";
            TempData["ToastrMessage"] = "Checklist realizado correctamente.";

            return RedirectToAction("ResueltoTicketChecklist", "Tickets", new { ticketId = ticketChecklist.TicketID });
        }

        /// <summary>
        /// Metodo para guardar los archivos en el servidor
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> GuardarArchivo(IFormFile archivoAdjunto)
        {
            if (archivoAdjunto != null && archivoAdjunto.Length > 0)
            {
                if (archivoAdjunto.Length > 5 * 1024 * 1024)
                {
                    return "Error";
                }

                var extensionesPermitidas = new[] { ".pdf", ".docx", ".xlsx", ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(archivoAdjunto.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(extension) || !extensionesPermitidas.Contains(extension))
                {
                    return "Error";
                }

                var nombreUnico =  Guid.NewGuid().ToString() + extension;
                var rutaDirectorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "checklist");

                if (!Directory.Exists(rutaDirectorio))
                {
                    Directory.CreateDirectory(rutaDirectorio);
                }

                var rutaCompleta = Path.Combine(rutaDirectorio, nombreUnico);
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await archivoAdjunto.CopyToAsync(stream);
                }

                return rutaCompleta;
            }

            return "Error";
        }

    }
}
