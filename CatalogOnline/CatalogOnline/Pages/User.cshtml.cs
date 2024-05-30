using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
namespace CatalogOnline.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersModel : PageModel
    {
        private readonly IUserService _usersService;

        public UsersModel(IUserService usersService)
        {
            _usersService = usersService;
        }

        public List<UserModel> Users { get; private set; }

        public void OnGet()
        {
            Users = _usersService.GetUserModels();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _usersService.DeleteUser(id);
            return RedirectToPage();
        }


    }
}
