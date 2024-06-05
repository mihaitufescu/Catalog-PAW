using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models.AuthentificationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "admin")]
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
        public UserRegisterModel User { get; set; }

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
                role = User.Role,
                year_of_study = User.Role == "student" ? User.YearOfStudy : null,
                group = User.Role == "student" ? User.Group : null
            };

            var res = userRepository.AddUser(newUser);
            if (res == null)
            {
                ModelState.AddModelError("AddUserError", "Failed to add user");
                return Page();
            }

            return RedirectToPage("User");
        }
    }
}
