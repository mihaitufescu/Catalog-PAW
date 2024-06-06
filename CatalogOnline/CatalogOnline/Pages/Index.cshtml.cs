using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
namespace CatalogOnline.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        public UserModel _user { get; set; }
        public string LoggedInUsername { get; set; }
        

        public IndexModel(ILogger<IndexModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                LoggedInUsername = User.Identity.Name;
                _user = _userService.GetUserByName(LoggedInUsername);
                
            }
        }
    }
}
