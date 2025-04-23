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
    public class TicketsController : Controller
    {
        private readonly TicketsContext _context;

        public TicketsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var ticketsContext = _context.Ticket.Include(t => t.Categoria).Include(t => t.Departamento).Include(t => t.Estado).Include(t => t.Prioridad).Include(t => t.UsuarioAsignado).Include(t => t.UsuarioReporte);
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
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Descripcion", ticket.CategoriaID);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "Descripcion", ticket.DepartamentoID);
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Descripcion", ticket.EstadoID);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Descripcion", ticket.PrioridadID);
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "Contrasena", ticket.UsuarioAsignadoID);
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "Contrasena", ticket.UsuarioReporteID);
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
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Descripcion", ticket.CategoriaID);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "Descripcion", ticket.DepartamentoID);
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Descripcion", ticket.EstadoID);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Descripcion", ticket.PrioridadID);
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "Contrasena", ticket.UsuarioAsignadoID);
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "Contrasena", ticket.UsuarioReporteID);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriaTicket, "CategoriaID", "Descripcion", ticket.CategoriaID);
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "Descripcion", ticket.DepartamentoID);
            ViewData["EstadoID"] = new SelectList(_context.EstadoTicket, "EstadoID", "Descripcion", ticket.EstadoID);
            ViewData["PrioridadID"] = new SelectList(_context.Prioridad, "PrioridadID", "Descripcion", ticket.PrioridadID);
            ViewData["UsuarioAsignadoID"] = new SelectList(_context.Usuario, "UsuarioID", "Contrasena", ticket.UsuarioAsignadoID);
            ViewData["UsuarioReporteID"] = new SelectList(_context.Usuario, "UsuarioID", "Contrasena", ticket.UsuarioReporteID);
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
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketID == id);
        }
    }
}
