using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
namespace CatalogOnline.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string LoggedInUsername { get; set; }
        public string LoggedId { get; set; }
        public string Role { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                LoggedInUsername = User.Identity.Name;
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    LoggedId = userIdClaim.Value;
                }

                var roleClaim = User.FindFirst(ClaimTypes.Role); // Use ClaimTypes.Role
                if (roleClaim != null)
                {
                    Role = roleClaim.Value;
                }
            }
        }
    }
}
