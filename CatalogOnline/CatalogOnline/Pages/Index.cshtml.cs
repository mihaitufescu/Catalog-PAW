using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string LoggedInUsername { get; set; }
        public string LoggedId { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                LoggedInUsername = User.Identity.Name;
                // Retrieve UserId claim
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    LoggedId = userIdClaim.Value;
                }
            }

        }
    }
}
