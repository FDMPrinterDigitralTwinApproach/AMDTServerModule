using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AMDTServerModule.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)] // This will prevent Swagger from generating docs for this controller
    public class SignalRTagController : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("Receive3DPrinterData", user, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
