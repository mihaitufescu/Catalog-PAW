using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CatalogOnline.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SendMessage(string user, string message, string title)
        {
            _logger.LogInformation($"Message from {user}: {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message, title);
        }
    }
}
