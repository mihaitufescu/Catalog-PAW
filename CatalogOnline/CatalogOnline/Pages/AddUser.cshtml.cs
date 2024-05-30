using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models.AuthentificationModels;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class AddUserModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<AddUserModel> logger;

        public AddUserModel(IUserRepository _userRepository, ILogger<AddUserModel> _logger)
        {
            userRepository = _userRepository;
            logger = _logger;
    }

        [BindProperty]
        public new UserRegisterModel User { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(User.Password);

            // Create a new user entity
            var newUser = new User
            {
                username = User.Username,
                legal_name = User.LegalName,
                email = User.Email,
                password = hashedPassword,
                role = User.Role
            };
            var res  =  userRepository.AddUser(newUser);
            if (res is null)
            {
                ModelState.AddModelError("AddUserError", "Failed to add user");
                return Page(); // Return the same page to display the error
            }

            return RedirectToPage("User"); // Corrected redirect
        }
    }
}
