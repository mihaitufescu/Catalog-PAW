using System.Text.RegularExpressions;
using CatalogOnline.DAL.DBO;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CatalogOnline.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _usersService;
        private readonly ILogger<EditUserModel> _logger;

        public EditUserModel(IUserService usersService, ILogger<EditUserModel> logger)
        {
            _usersService = usersService;
            _logger = logger;
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

        public async Task<IActionResult> OnPostAsync()
        {
            bool passwordChanged = false;
            // Check if the password is provided and valid
            if (string.IsNullOrEmpty(User.password))
            {
                TempData["PasswordMessage"] = "Password was not changed.";
            }
            else
            {
                if (!IsValidPassword(User.password))
                {
                    TempData["PasswordMessage"] = "Password was not changed. The password should have 8 characters length, 1 letter, 1 number and 1 special character.";
                }
                else
                {
                    passwordChanged = true;
                }
            }
            if (!IsValidEmail(User.email))
            {
                ModelState.AddModelError("User.email", "Invalid email format.");
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
            if (passwordChanged)
            {
                userToUpdate.password = BCrypt.Net.BCrypt.HashPassword(User.password);
                TempData["PasswordMessage"] = "Password was succesfully changed.";
            }
            var res = await _usersService.UpdateUser(userToUpdate);
            if (!res)
            {
                ModelState.AddModelError("EditUserError", "Failed to edit user");
            }

            return Page();
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression to check if the email format is valid
            var regex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return regex.IsMatch(email);
        }
        private bool IsValidPassword(string password)
        {
            // Regular expression to check if the password contains at least one letter, one number, and one special character, and is at least 8 characters long
            var regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}
