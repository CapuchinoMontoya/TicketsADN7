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
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

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
                        .Where(m => m.Activo && m.FechaProximaRevision <= fechaLimite)
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
                                RutaArchivo = "Error",
                                EstadoID = 1,
                                CategoriaID = 1,
                                PrioridadID = 1,
                                UsuarioReporteID = 1,
                                DepartamentoID = 3,
                            };

                            context.Ticket.Add(ticket);
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) { 
                Debug.WriteLine(ex);
            }
        }
    }
}
