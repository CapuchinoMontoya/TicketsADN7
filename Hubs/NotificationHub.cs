using Microsoft.AspNetCore.SignalR;

namespace TicketsADN7.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task JoinNotificationGroup(string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);
        }
    }
}
