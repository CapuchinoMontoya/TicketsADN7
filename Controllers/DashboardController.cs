using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTELISIS.APPCORE.EL;
using TicketsADN7.Models;
using Microsoft.AspNetCore.Authorization;

namespace TicketsADN7.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly TicketsContext _context;

        public DashboardController(TicketsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Obtener datos de la base de datos
            var totalTickets = _context.Ticket.Count();
            var ticketsAbiertos = _context.Ticket.Count(t => t.EstadoID == 1); // Asumiendo que 1 es "Abierto"
            var ticketsEnProgreso = _context.Ticket.Count(t => t.EstadoID == 2); // Asumiendo que 2 es "En Progreso"
            var ticketsCerrados = _context.Ticket.Count(t => t.Estado.EsCerrado);

            // Calcular porcentaje de tickets en progreso
            var porcentajeEnProgreso = totalTickets > 0 ? (ticketsEnProgreso * 100) / totalTickets : 0;

            // Tickets por mes (últimos 6 meses)
            var fechaInicio = DateTime.Now.AddMonths(-6);
            var ticketsPorMes = Enumerable.Range(0, 6)
                .Select(i => _context.Ticket
                    .Count(t => t.FechaCreacion.Month == fechaInicio.AddMonths(i).Month &&
                               t.FechaCreacion.Year == fechaInicio.AddMonths(i).Year))
                .ToList();

            var meses = Enumerable.Range(0, 6)
                .Select(i => fechaInicio.AddMonths(i).ToString("MMM yyyy"))
                .ToList();

            // Distribución por prioridad
            var prioridades = _context.Prioridad.OrderBy(p => p.Nivel).ToList();
            var prioridadesData = prioridades
                .Select(p => _context.Ticket.Count(t => t.PrioridadID == p.PrioridadID))
                .ToList();
            var prioridadesLabels = prioridades.Select(p => p.Nombre).ToList();

            // Tickets por categoría
            var ticketsPorCategoria = _context.CategoriaTicket
                .Select(c => new {
                    c.Nombre,
                    Cantidad = _context.Ticket.Count(t => t.CategoriaID == c.CategoriaID),
                    Porcentaje = totalTickets > 0 ? (_context.Ticket.Count(t => t.CategoriaID == c.CategoriaID) * 100) / totalTickets : 0
                })
                .ToList();

            // Tickets por departamento
            var departamentos = _context.Departamento.ToList();
            var departamentosData = departamentos
                .Select(d => _context.Ticket.Count(t => t.DepartamentoID == d.DepartamentoID))
                .ToList();
            var departamentosLabels = departamentos.Select(d => d.NombreDepartamento).ToList();

            // Últimos tickets
            var ultimosTickets = _context.Ticket
                .Include(t => t.Estado)
                .OrderByDescending(t => t.FechaCreacion)
                .Take(5)
                .ToList();

            // Tiempo promedio de resolución
            var ticketsResueltos = _context.Ticket
                .Where(t => t.Estado.EsCerrado)
                .ToList();

            var tiempoPromedio = ticketsResueltos.Any() ?
                ticketsResueltos.Average(t => (t.FechaCierre - t.FechaCreacion).TotalDays) : 0;

            var ticketsRapidos = ticketsResueltos.Any() ?
                (ticketsResueltos.Count(t => (t.FechaCierre - t.FechaCreacion).TotalDays <= 3) * 100 / ticketsResueltos.Count ): 0;

            // Pasar datos a la vista
            ViewBag.TotalTickets = totalTickets;
            ViewBag.TicketsAbiertos = ticketsAbiertos;
            ViewBag.TicketsEnProgreso = ticketsEnProgreso;
            ViewBag.PorcentajeEnProgreso = porcentajeEnProgreso;
            ViewBag.TicketsCerrados = ticketsCerrados;
            ViewBag.TicketsPorMes = ticketsPorMes;
            ViewBag.Meses = meses;
            ViewBag.PrioridadesData = prioridadesData;
            ViewBag.PrioridadesLabels = prioridadesLabels;
            ViewBag.TicketsPorCategoria = ticketsPorCategoria;
            ViewBag.DepartamentosData = departamentosData;
            ViewBag.DepartamentosLabels = departamentosLabels;
            ViewBag.UltimosTickets = ultimosTickets;
            ViewBag.TiempoPromedioResolucion = Math.Round(tiempoPromedio, 1);
            ViewBag.TicketsRapidos = ticketsRapidos;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var tickets = await _context.Ticket
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .Include(t => t.Departamento)
                .ToListAsync();

            var data = new
            {
                ticketsPorEstado = tickets
                    .GroupBy(t => t.Estado.Nombre)
                    .Select(g => new { Estado = g.Key, Total = g.Count() }),

                ticketsPorDepartamento = tickets
                    .Where(t => t.Departamento != null)
                    .GroupBy(t => t.Departamento.NombreDepartamento)
                    .Select(g => new { Departamento = g.Key, Total = g.Count() })
                    .OrderByDescending(g => g.Total)
                    .Take(5),

                tiempoPromedioResolucion = tickets
                    .Where(t => t.Estado.EsCerrado)
                    .Select(t => (t.FechaCierre - t.FechaCreacion).TotalDays)
                    .DefaultIfEmpty(0)
                    .Average(),

                ticketsPorPrioridad = tickets
                    .GroupBy(t => t.Prioridad.Nombre)
                    .Select(g => new { Prioridad = g.Key, Total = g.Count() }),


                ultimosTickets = tickets
                    .OrderByDescending(t => t.FechaCreacion)
                    .Take(5)
                    .Select(t => new
                    {
                        t.Titulo,
                        Estado = t.Estado.Nombre,
                        Prioridad = t.Prioridad.Nombre,
                        Fecha = t.FechaCreacion.ToString("dd/MM/yyyy")
                    })
            };

            return Json(data);
        }
    }
}
