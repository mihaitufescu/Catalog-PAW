using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CatalogOnline.Pages.Chat
{
    [Authorize(Roles = "secretary,teacher")] // Only users with roles "secretar" or "teacher" can access this page
    public class ChatModel : PageModel
    {
        private readonly ILogger<ChatModel> _logger;
        public string Username { get; private set; }
        public ChatModel(ILogger<ChatModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Username = User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
