using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebStore.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string Message) => await Clients.Others.SendAsync("MessageFromClient", Message);
    }
}
