using DocumentFormat.OpenXml.InkML;
using INTELISIS.APPCORE.EL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TicketsADN7.Models;

namespace TicketsADN7.Services
{
    public class TicketGeneratorService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TicketGeneratorService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GenerarTickets();
                await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);

            }
        }

        private async Task GenerarTickets()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TicketsContext>();

                    var fechaLimite = DateTime.Today.AddDays(3);
                    var mantenimientosPendientes = await context.MantenimientoProgramado
                    .Where(m => m.Activo &&
                                m.FechaProximaRevision >= DateTime.Today &&
                                m.FechaProximaRevision <= fechaLimite)
                    .ToListAsync();


                    foreach (var mantenimiento in mantenimientosPendientes)
                    {
                        bool ticketYaExiste = await context.Ticket
                            .AnyAsync(t => t.Titulo == $"Mantenimiento pendiente: {mantenimiento.Nombre}" && t.Descripcion == $"Mantenimiento programado para {mantenimiento.FechaProximaRevision:dd/MM/yyyy}");

                        if (!ticketYaExiste)
                        {
                            var ticket = new Ticket
                            {
                                Titulo = $"Mantenimiento pendiente: {mantenimiento.Nombre}",
                                Descripcion = $"Mantenimiento programado para {mantenimiento.FechaProximaRevision:dd/MM/yyyy}",
                                FechaCreacion = DateTime.Now,
                                RutaArchivo = null,
                                EstadoID = 1,
                                CategoriaID = Convert.ToInt32(mantenimiento.Categoria),
                                PrioridadID = 4,
                                UsuarioReporteID = 1, //Por default el Admin
                                DepartamentoID = Convert.ToInt32(mantenimiento.Departamento),
                            };
                            context.Add(ticket);
                            await context.SaveChangesAsync();

                            if (mantenimiento.ChecklistId is not null)
                            {
                                var ticketChecklist = new TicketChecklist
                                {
                                    TicketID = ticket.TicketID,
                                    ChecklistID = (int)mantenimiento.ChecklistId,
                                };
                                context.Add(ticketChecklist);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex) { 
                Debug.WriteLine(ex);
            }
        }
    }
}
