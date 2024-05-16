using System;
using System.Threading.Tasks;
using CatalogOnline.DAL;
using CatalogOnline.DAL.DBO;
using CatalogOnline.Models.AuthentificationModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CatalogOnline.Pages.Authentification
{
    public class RegisterModel : PageModel
    {
        private readonly DataContext _context;

        [BindProperty]
        public UserRegisterModel User { get; set; }

        public RegisterModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if the username already exists
                    var existingUser = await _context.User.FirstOrDefaultAsync(u => u.username.ToLower() == User.Username.ToLower());
                    if (existingUser != null)
                    {
                        ModelState.AddModelError(string.Empty, "Username already exists");
                        return Page();
                    }

                    // Encrypt the password using bcrypt
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

                    // Add the new user to the database
                    _context.User.Add(newUser);
                    await _context.SaveChangesAsync();

                    // Redirect to the login page after successful registration
                    return RedirectToPage("/Authentification/Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error registering the account: " + ex.Message);
                }
            }
            return Page();
        }

    }
}
