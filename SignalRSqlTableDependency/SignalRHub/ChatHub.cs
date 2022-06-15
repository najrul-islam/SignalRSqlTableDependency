using Microsoft.AspNetCore.SignalR;

namespace SignalRSqlTableDependency.SignalRHub
{
    public class ChatHub: Hub<IChatHubClient>
    {
        public async Task SendProductUpdate()
        {
            await Clients.All.onProductUpdate("Quantity updated.");
        }
    }
}
