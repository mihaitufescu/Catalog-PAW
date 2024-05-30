using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using CatalogOnline.DAL.Repository.Interfaces;
namespace CatalogOnline.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersModel : PageModel
    {
        private readonly IUserService _usersService;
        private readonly IUserRepository userRepository;
        public UsersModel(IUserService usersService, IUserRepository userRepository)
        {
            _usersService = usersService;
            this.userRepository = userRepository;
        }

        public List<UserModel> Users { get; private set; }

        public void OnGet()
        {
            Users = _usersService.GetUserModels();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            userRepository.DeleteUser(id);
            return RedirectToPage();
        }


    }
}
