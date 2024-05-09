using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;

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
            Users = _usersService.GetUsers();
        }
    }
}
