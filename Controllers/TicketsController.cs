using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using EmailService;
using TicketsADN7.Services;
using EMAILSERVICES;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using WHATSAPPSERVICES;
using System.Linq;

namespace TicketsADN7.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketsContext _context;
        private readonly ILogger<TicketsController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly EmailConfiguration _emailConfig;
        private readonly IWhatsAppSender _whatsAppSender;
        private readonly INotificationService _notificationService;

        public TicketsController(TicketsContext context, ILogger<TicketsController> logger, IViewRenderService viewRenderService, IOptions<EmailConfiguration> emailConfig, IWhatsAppSender whatsAppSender, INotificationService notificationService)
        {
            _context = context;
            _logger = logger;
            _viewRenderService = viewRenderService;
            _emailConfig = emailConfig.Value;
            _whatsAppSender = whatsAppSender;
            _notificationService = notificationService;
        }

        // GET: Tickets
        public async Task<IActionResult> MisTickets()
        {
            var user = HttpContext.User;
            var userName = user.Identity.Name.ToString();

            var ticketsContext = _context.Ticket.Include(t => t.Categoria).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridad).Include(t => t.UsuarioAsignado).Include(t => t.UsuarioReporte).Where(t => t.UsuarioReporte.NombreUsuario == userName);
            return View(await ticketsContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Categoria)
                .Include(t => t.Departamento)
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.UsuarioReporte)
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Nombre");
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento");
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Nombre");
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Nombre");
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario");
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario");
            ViewBag.Checklists = new SelectList(_context.Checklist.ToList(), "ChecklistID", "Nombre");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Titulo,Descripcion,RutaArchivo,FechaCreacion,FechaCierre,CategoriaID,PrioridadID,EstadoID,UsuarioReporteID,UsuarioAsignadoID,DepartamentoID")] Ticket ticket, int? checklistId, IFormFile? archivoAdjunto)
        {
            var user = HttpContext.User;
            var userName = user.Identity.Name.ToString();

            ModelState.Remove("Categoria");
            ModelState.Remove("Departamento");
            ModelState.Remove("Estado");
            ModelState.Remove("Prioridad");
            ModelState.Remove("UsuarioAsignado");
            ModelState.Remove("UsuarioReporte");
            ModelState.Remove("RutaArchivo");
            ModelState.Remove("TicketChecklist");
            if (ModelState.IsValid)
            {
                var archivoUpload = await GuardarArchivo(archivoAdjunto, ticket.Titulo, ticket.TicketID.ToString());

                ticket.RutaArchivo = archivoUpload != "Error" ? archivoUpload : null;

                //Prioridad baja por defecto
                ticket.PrioridadID = 4;

                ticket.UsuarioReporteID = await _context.Usuario.Where(x => x.NombreUsuario == userName).Select(x => x.UsuarioID).FirstOrDefaultAsync();

                _context.Add(ticket);
                await _context.SaveChangesAsync();

                if (checklistId.HasValue)
                {
                    var ticketChecklist = new TicketChecklist
                    {
                        TicketID = ticket.TicketID,
                        ChecklistID = checklistId.Value
                    };
                    _context.TicketChecklist.Add(ticketChecklist);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(MisTickets));
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Nombre", ticket.CategoriaID);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento", ticket.DepartamentoID);
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Nombre", ticket.EstadoID);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Nombre", ticket.PrioridadID);
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", ticket.UsuarioAsignadoID);
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", ticket.UsuarioReporteID);
            ViewBag.Checklists = new SelectList(await _context.Checklist.ToListAsync(), "ChecklistID", "Nombre", checklistId);
            return View(ticket);
        }
        
        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Nombre", ticket.CategoriaID);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento", ticket.DepartamentoID);
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Nombre", ticket.EstadoID);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Nombre", ticket.PrioridadID);
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", ticket.UsuarioAsignadoID);
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", ticket.UsuarioReporteID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,Titulo,Descripcion,RutaArchivo,FechaCreacion,FechaCierre,CategoriaID,PrioridadID,EstadoID,UsuarioReporteID,UsuarioAsignadoID,DepartamentoID")] Ticket ticket)
        {
            if (id != ticket.TicketID)
            {
                return NotFound();
            }
            ModelState.Remove("Categoria");
            ModelState.Remove("Departamento");
            ModelState.Remove("Estado");
            ModelState.Remove("Prioridad");
            ModelState.Remove("UsuarioAsignado");
            ModelState.Remove("UsuarioReporte");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MisTickets));
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Nombre", ticket.CategoriaID);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "NombreDepartamento", ticket.DepartamentoID);
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Nombre", ticket.EstadoID);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Nombre", ticket.PrioridadID);
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", ticket.UsuarioAsignadoID);
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario", ticket.UsuarioReporteID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Categoria)
                .Include(t => t.Departamento)
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.UsuarioReporte)
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MisTickets));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketID == id);
        }

        /// <summary>
        /// Metodo para retornar todos los tickets que no esten rechazados o cerrados.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> GestionarTickets()
        {
            var ticketsContext = _context.Ticket.Include(t => t.Categoria).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridad).Include(t => t.UsuarioAsignado).Include(t => t.UsuarioReporte).Where(t => t.EstadoID != 5 && t.EstadoID != 6);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Nombre");
            ViewData["UsuarioAsignadoID"] = await _context.Usuario
            .Where(x => x.RolID != 3)
            .Select(x => new SelectListItem
            {
                Value = x.UsuarioID.ToString(),
                Text = x.NombreUsuario + " (" + x.NombreCompleto + ")"
            })
            .ToListAsync();

            return View(await ticketsContext.ToListAsync());
        }

        /// <summary>
        /// Metodo para autorizar y asiganar un ticket a un usuario
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a asiganar</param>
        /// <param name="usuarioAsignadoId">Usuario al que se le va asiganar el ticket</param>
        /// <param name="prioridadId">Prioridad del ticket que se va a autorizar</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> AsignarTicket(int ticketId, int usuarioAsignadoId, int prioridadId)
        {
            try
            {
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket no encontrado" });
                }
                var usuario = await _context.Usuario.FindAsync(usuarioAsignadoId);
                if (usuario == null)
                {
                    return BadRequest(new { success = false, message = "Usuario asignado no válido" });
                }
                
                ticket.UsuarioAsignadoID = usuarioAsignadoId;
                ticket.PrioridadID = prioridadId;
                ticket.EstadoID = 2;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Ticket asignado correctamente a {usuario.NombreUsuario}";

                // Notificar al usuario asignado
                var notification = $"Se te ha asignado el ticket #{ticket.TicketID}";
                await _notificationService.SendNotificationToUser(
                    usuario.NombreUsuario,
                    notification,
                    ticket.TicketID);

                //Logica para mandar correo electronico
                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == usuarioAsignadoId);

                //Notificar por correo electronico
                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Nueva Asignación de Ticket",
                    Mensaje = "Se te ha asignado un nuevo ticket para su atención y seguimiento. Te invitamos a ingresar al sistema y revisar los detalles cuanto antes.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(
                    _emailConfig,
                    usuarioAsignado.Email,
                    "Asigancion de tickets",
                    body
                );

                //Notificar por Whatsapp
                var message = new WhatsAppMessage(
                        to: usuarioAsignado.Telefono,
                        body: "Se te ha asignado un nuevo ticket para su atención y seguimiento. Te invitamos a ingresar al sistema y revisar los detalles cuanto antes."
                    );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(GestionarTickets));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar ticket");
                return StatusCode(500, new { success = false, message = "Error interno al asignar ticket" });
            }
        }

        /// <summary>
        /// Metodo para rechazar un ticket
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a rechazar</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> RechazarTicket(int ticketId)
        {
            try
            {
                //Buscar ticket en la base de datos
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket no encontrado" });
                }
                
                //Se le asigna por el estatus 6 al ticket (Rechazado)
                ticket.EstadoID = 6;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Ticket rechazado correctamente";

                //Logica para mandar correo electronico
                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                //Notificar por correo electronico
                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket Rechazado",
                    Mensaje = "El ticket ha sido rechazado. Te invitamos a revisar los detalles en el sistema y tomar las acciones correspondientes si es necesario.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(
                    _emailConfig,
                    usuarioAsignado.Email,
                    "Asigancion de tickets",
                    body
                );

                //Notificar por Whatsapp
                var message = new WhatsAppMessage(
                        to: usuarioAsignado.Telefono,
                        body: "El ticket ha sido rechazado. Te invitamos a revisar los detalles en el sistema y tomar las acciones correspondientes si es necesario."
                    );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(GestionarTickets));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar ticket");
                return StatusCode(500, new { success = false, message = "Error interno al asignar ticket" });
            }
        }

        /// <summary>
        /// Metodo para poner finalizar un ticket
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a finalizar</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> FinalizarTicket(int ticketId)
        {
            try
            {
                //Buscar ticket en la base de datos
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket no encontrado" });
                }

                //Se le asigna por el estatus 5 al ticket (Cerrado)
                ticket.EstadoID = 5;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Ticket finalizado";

                //Logica para mandar correo electronico
                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                //Notificar por correo electronico
                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket finalizado",
                    Mensaje = "El ticket ha sido cerrado exitosamente. Puedes consultar los detalles y el historial en el sistema para futuras referencias.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(
                    _emailConfig,
                    usuarioAsignado.Email,
                    "Asigancion de tickets",
                    body
                );

                //Notificar por Whatsapp
                var message = new WhatsAppMessage(
                        to: usuarioAsignado.Telefono,
                        body: "El ticket ha sido cerrado exitosamente. Puedes consultar los detalles y el historial en el sistema para futuras referencias."
                    );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(MisTicketsAsigandos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar ticket");
                return StatusCode(500, new { success = false, message = "Error interno al asignar ticket" });
            }
        }

        /// <summary>
        /// Metodo para retornar los ticktes asigandos al usuario autentificado
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> MisTicketsAsigandos()
        {
            var user = HttpContext.User;
            var userName = user.Identity.Name.ToString();
            var ticketsContext = _context.Ticket
                .Include(t => t.Categoria)
                .Include(t => t.Departamento)
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.UsuarioReporte)
                .Include(t => t.TicketChecklist)
                .Where(t => t.UsuarioAsignado.NombreUsuario == userName && t.EstadoID != 4 && t.EstadoID != 5 && t.EstadoID != 6);
            return View(await ticketsContext.ToListAsync());
        }

        /// <summary>
        /// Metodo para poner en espera un ticket
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a poner en espera</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> EnEsperaTicket(int ticketId)
        {
            try
            {
                //Buscar ticket en la base de datos
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket no encontrado" });
                }

                //Se le asigna por el estatus 3 al ticket (En Espera)
                ticket.EstadoID = 3;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "info";
                TempData["ToastrMessage"] = $"Ticket En Espera";

                //Logica para mandar correo electronico
                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                //Notificar por correo electronico
                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket En Espera",
                    Mensaje = "El ticket se encuentra en pausa.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(
                    _emailConfig,
                    usuarioAsignado.Email,
                    "Ticket En Espera",
                    body
                );

                //Notificar por Whatsapp
                var message = new WhatsAppMessage(
                        to: usuarioAsignado.Telefono,
                        body: "El ticket se encuentra en pausa."
                    );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(MisTicketsAsigandos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar ticket");
                return StatusCode(500, new { success = false, message = "Error interno al asignar ticket" });
            }
        }

        /// <summary>
        /// Metodo para poner en proceso un ticket
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a poner en proceso</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> EnProcesoTicket(int ticketId)
        {
            try
            {
                //Buscar ticket en la base de datos
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket no encontrado" });
                }

                //Se le asigna por el estatus 2 al ticket (En Proceso)
                ticket.EstadoID = 2;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Ticket En Proceso";

                //Logica para mandar correo electronico
                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                //Notificar por correo electronico
                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket En Proceso",
                    Mensaje = "El ticket está actualmente en proceso.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(
                    _emailConfig,
                    usuarioAsignado.Email,
                    "Ticket En Proceso",
                    body
                );

                //Notificar por Whatsapp
                var message = new WhatsAppMessage(
                        to: usuarioAsignado.Telefono,
                        body: "El ticket está actualmente en proceso."
                    );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(MisTicketsAsigandos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar ticket");
                return StatusCode(500, new { success = false, message = "Error interno al asignar ticket" });
            }
        }

        /// <summary>
        /// Metodo para poner en estatus resuelto un ticket sin checklist
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a poner en estatus resuelto</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> ResueltoTicket(int ticketId)
        {
            try
            {
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                    return NotFound(new { success = false, message = "Ticket no encontrado" });

                // Verificar si el ticket tiene un checklist asignado
                var ticketChecklist = await _context.TicketChecklist
                .Include(tc => tc.Checklist)
                .FirstOrDefaultAsync(tc => tc.TicketID == ticketId);

                if (ticketChecklist != null)
                {
                    int ticketChecklistId = ticketChecklist.TicketChecklistID;

                    // Obtener todos los campos del checklist asignado
                    var camposChecklist = await _context.ChecklistCampo
                        .Where(c => c.ChecklistID == 1)
                        .ToListAsync();

                    int totalCampos = camposChecklist.Count;

                    // Obtener las respuestas que se han dado a ese checklist en este ticket
                    var respuestas = await _context.RespuestaChecklistCampo
                        .Where(r => r.TicketChecklistID == ticketChecklistId)
                        .ToListAsync();

                    int camposRespondidos = respuestas
                        .Where(r => !string.IsNullOrWhiteSpace(r.Valor))
                        .Select(r => r.ChecklistCampoID)
                        .Distinct()
                        .Count();

                    if (camposRespondidos < totalCampos)
                    {
                        TempData["ToastrType"] = "warning";
                        TempData["ToastrMessage"] = "No se puede resolver el ticket. Aún hay campos del checklist sin completar.";
                        return RedirectToAction(nameof(MisTicketsAsigandos));
                    }
                }


                // Cambiar estado a "Resuelto" (ID = 4)
                ticket.EstadoID = 4;
                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = "Ticket Resuelto";

                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket Resuelto",
                    Mensaje = "El ticket ha sido resuelto y está en espera de validación.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(_emailConfig, usuarioAsignado.Email, "Ticket Resuelto", body);

                var message = new WhatsAppMessage(
                    to: usuarioAsignado.Telefono,
                    body: "El ticket ha sido resuelto y está en espera de validación."
                );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(MisTicketsAsigandos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al resolver ticket");
                return StatusCode(500, new { success = false, message = "Error interno al resolver ticket" });
            }
        }

        /// <summary>
        /// Metodo para poner en estatus resuelto un ticket con checklist
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a poner en estatus resuelto</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<IActionResult> ResueltoTicketChecklist(int ticketId)
        {
            try
            {
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                    return NotFound(new { success = false, message = "Ticket no encontrado" });

                // Verificar si el ticket tiene un checklist asignado
                var ticketChecklist = await _context.TicketChecklist
                .Include(tc => tc.Checklist)
                .FirstOrDefaultAsync(tc => tc.TicketID == ticketId);

                if (ticketChecklist != null)
                {
                    int checklistId = ticketChecklist.ChecklistID;

                    // Obtener todos los campos del checklist asignado
                    var camposChecklist = await _context.ChecklistCampo
                        .Where(c => c.ChecklistID == checklistId)
                        .ToListAsync();

                    int totalCampos = camposChecklist.Count;

                    // Obtener las respuestas que se han dado a ese checklist en este ticket
                    var respuestas = await _context.RespuestaChecklistCampo
                        .Where(r => r.TicketChecklistID == ticketChecklist.TicketChecklistID)
                        .ToListAsync();

                    int camposRespondidos = respuestas
                        .Where(r => !string.IsNullOrWhiteSpace(r.Valor))
                        .Select(r => r.ChecklistCampoID)
                        .Distinct()
                        .Count();

                    if (camposRespondidos < totalCampos)
                    {
                        TempData["ToastrType"] = "warning";
                        TempData["ToastrMessage"] = "No se puede resolver el ticket. Aún hay campos del checklist sin completar.";
                        return RedirectToAction(nameof(MisTicketsAsigandos));
                    }
                }


                // Cambiar estado a "Resuelto" (ID = 4)
                ticket.EstadoID = 4;
                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = "Ticket Resuelto";

                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket Resuelto",
                    Mensaje = "El ticket ha sido resuelto y está en espera de validación.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(_emailConfig, usuarioAsignado.Email, "Ticket Resuelto", body);

                var message = new WhatsAppMessage(
                    to: usuarioAsignado.Telefono,
                    body: "El ticket ha sido resuelto y está en espera de validación."
                );

                bool result = _whatsAppSender.SendWhatsAppMessage(message);

                return RedirectToAction(nameof(MisTicketsAsigandos));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al resolver ticket");
                return StatusCode(500, new { success = false, message = "Error interno al resolver ticket" });
            }
        }


        /// <summary>
        /// Metodo para retornar todos los ticktes
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> HistorialTickets()
        {
            var ticketsContext = _context.Ticket.Include(t => t.Categoria).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridad).Include(t => t.UsuarioAsignado).Include(t => t.UsuarioReporte);
            return View(await ticketsContext.ToListAsync());
        }

        /// <summary>
        /// Metodo para poner finalizar un ticket
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a finalizar</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost] // Cambiado a GET ya que solo muestra información
        public async Task<IActionResult> RevisarChecklistTicket(int ticketId)
        {
            try
            {
                // Obtener el ticketChecklist con toda la información relacionada
                var ticketChecklist = await _context.TicketChecklist
                    .Include(tc => tc.Checklist)
                        .ThenInclude(c => c.Campos)
                    .Include(tc => tc.Respuestas)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticketChecklist == null)
                {
                    TempData["ToastrType"] = "warning";
                    TempData["ToastrMessage"] = "No se encontró checklist para este ticket";
                    return RedirectToAction("GestionarTickets");
                }

                // Crear modelo para la vista
                var model = new ChecklistContestadoViewModel
                {
                    TicketId = ticketId,
                    NombreChecklist = ticketChecklist.Checklist.Nombre,
                    DescripcionChecklist = ticketChecklist.Checklist.Descripcion,
                    Campos = ticketChecklist.Checklist.Campos.Select(c => new CampoChecklistContestado
                    {
                        Nombre = c.NombreCampo,
                        Tipo = c.Tipo,
                        RequiereEvidencia = c.RequiereEvidencia,
                        Valor = ticketChecklist.Respuestas.FirstOrDefault(r => r.ChecklistCampoID == c.ChecklistCampoID)?.Valor
                    }).ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al revisar ticket");
                TempData["ToastrType"] = "error";
                TempData["ToastrMessage"] = "Error interno al revisar el checklist";
                return RedirectToAction("DetallesTicket", new { id = ticketId });
            }
        }

        /// <summary>
        /// Metodo para guardar los archivos en el servidor
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> GuardarArchivo(IFormFile archivoAdjunto, string titulo, string id)
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

                var nombreUnico = titulo + "-" +Guid.NewGuid().ToString() + extension;
                var rutaDirectorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

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
