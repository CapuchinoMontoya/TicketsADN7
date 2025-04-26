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

namespace TicketsADN7.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketsContext _context;
        private readonly ILogger<TicketsController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly EmailConfiguration _emailConfig;
        private readonly IWhatsAppSender _whatsAppSender;

        public TicketsController(TicketsContext context, ILogger<TicketsController> logger, IViewRenderService viewRenderService, IOptions<EmailConfiguration> emailConfig, IWhatsAppSender whatsAppSender)
        {
            _context = context;
            _logger = logger;
            _viewRenderService = viewRenderService;
            _emailConfig = emailConfig.Value;
            _whatsAppSender = whatsAppSender;
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
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Titulo,Descripcion,RutaArchivo,FechaCreacion,FechaCierre,CategoriaID,PrioridadID,EstadoID,UsuarioReporteID,UsuarioAsignadoID,DepartamentoID")] Ticket ticket)
        {
            ModelState.Remove("Categoria");
            ModelState.Remove("Departamento");
            ModelState.Remove("Estado");
            ModelState.Remove("Prioridad");
            ModelState.Remove("UsuarioAsignado");
            ModelState.Remove("UsuarioReporte");
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
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
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "NombreUsuario");
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
                //Buscar ticket en la base de datos
                var ticket = await _context.Ticket
                    .Include(t => t.Estado)
                    .FirstOrDefaultAsync(t => t.TicketID == ticketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket no encontrado" });
                }
                //Buscar usuario al que se le va asigar el ticket
                var usuario = await _context.Usuario.FindAsync(usuarioAsignadoId);
                if (usuario == null)
                {
                    return BadRequest(new { success = false, message = "Usuario asignado no válido" });
                }
                
                ticket.UsuarioAsignadoID = usuarioAsignadoId;
                ticket.PrioridadID = prioridadId;
                //Se le asigna por dafult el estatus 2 al ticket (En proceso)
                ticket.EstadoID = 2;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Ticket asignado correctamente a {usuario.NombreUsuario}";

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
            var ticketsContext = _context.Ticket.Include(t => t.Categoria).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridad).Include(t => t.UsuarioAsignado).Include(t => t.UsuarioReporte).Where(t => t.UsuarioAsignado.NombreUsuario == userName && t.EstadoID != 4 && t.EstadoID != 5 && t.EstadoID != 6);
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
        /// Metodo para poner en estatus resuelto un ticket
        /// </summary>
        /// <param name="ticketId">Id del ticket que se va a poner en estatus resuelto</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> ResueltoTicket(int ticketId)
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

                //Se le asigna por el estatus 4 al ticket (Resuelto)
                ticket.EstadoID = 4;

                await _context.SaveChangesAsync();

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = $"Ticket Resuelto";

                //Logica para mandar correo electronico
                var usuarioAsignado = await _context.Usuario
                    .FirstOrDefaultAsync(t => t.UsuarioID == ticket.UsuarioReporteID);

                //Notificar por correo electronico
                var model = new CorreoGenerico
                {
                    NombreUsuario = usuarioAsignado.NombreCompleto,
                    Titulo = "Ticket Resuelto",
                    Mensaje = "El ticket ha sido resuelto y esta en espera de validacion.",
                    Url = "https://localhost:7210/Tickets/MisTickets",
                    TextoBoton = "Ver Ticket Asignado",
                    ColorFondo = "#007bff"
                };

                string body = await _viewRenderService.RenderToStringAsync("Emails/Asignacion", model);

                EmailHelper.EnviarCorreo(
                    _emailConfig,
                    usuarioAsignado.Email,
                    "Ticket Resuelto",
                    body
                );

                //Notificar por Whatsapp
                var message = new WhatsAppMessage(
                        to: usuarioAsignado.Telefono,
                        body: "El ticket ha sido resuelto y esta en espera de validacion."
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
        /// Metodo para retornar todos los ticktes
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> HistorialTickets()
        {
            var ticketsContext = _context.Ticket.Include(t => t.Categoria).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridad).Include(t => t.UsuarioAsignado).Include(t => t.UsuarioReporte);
            return View(await ticketsContext.ToListAsync());
        }
    }
}
