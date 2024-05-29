using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
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
