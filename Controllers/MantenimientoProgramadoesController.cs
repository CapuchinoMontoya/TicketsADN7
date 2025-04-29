using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;

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
        public async Task<IActionResult> Create([Bind("Id,Nombre,Categoria,FrecuenciaDias,FechaUltimaRevision,FechaProximaRevision,Activo")] MantenimientoProgramado mantenimientoProgramado)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Categoria,FrecuenciaDias,FechaUltimaRevision,FechaProximaRevision,Activo")] MantenimientoProgramado mantenimientoProgramado)
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
            var eventos = await _context.MantenimientoProgramado
                .Where(m => m.Activo)
                .Select(m => new {
                    title = m.Nombre + " (" + m.Categoria + ")",
                    start = m.FechaProximaRevision.ToString("yyyy-MM-dd"),
                    allDay = true
                })
                .ToListAsync();

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

                if ((int)campo.Tipo == 1) // Checkbox
                {
                    valor = Request.Form[inputName].FirstOrDefault() == "on" ? "true" : "false";
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
            TempData["ToastrMessage"] = "Checklist guardado correctamente.";

            return RedirectToAction("ResueltoTicket", "Tickets", new { ticketId = ticketChecklist.TicketID });
        }

    }
}
