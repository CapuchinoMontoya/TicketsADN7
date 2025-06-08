using Microsoft.AspNetCore.SignalR;
using TicketsADN7.Hubs;

namespace TicketsADN7.Services
{
    public interface INotificationService
    {
        Task SendNotificationToUser(string userName, string message, int ticketId);
        Task SendStatusChangeNotification(string userName, string message, int ticketId, string newStatus);
    }

    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationToUser(string userName, string message, int ticketId)
        {
            await _hubContext.Clients.Group(userName).SendAsync("ReceiveNotification", message, ticketId);
        }

        public async Task SendStatusChangeNotification(string userName, string message, int ticketId, string newStatus)
        {
            await _hubContext.Clients.Group(userName).SendAsync("ReceiveStatusChange", message, ticketId, newStatus);
        }
    }
}
