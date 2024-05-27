using CatalogOnline.DAL.DBO;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _usersService;

        public EditUserModel(IUserService usersService)
        {
            _usersService = usersService;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int id)
        {
            User = _usersService.GetUserById(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userToUpdate = _usersService.GetUserById(User.user_id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.username = User.username;
            userToUpdate.legal_name = User.legal_name;
            userToUpdate.email = User.email;
            userToUpdate.role = User.role;

            _usersService.UpdateUser(userToUpdate);

            return RedirectToPage("/User");
        }
    }
}
