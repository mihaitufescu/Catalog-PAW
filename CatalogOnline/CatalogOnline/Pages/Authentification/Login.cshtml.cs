using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CatalogOnline.DAL;
using CatalogOnline.DAL.DBO;
using CatalogOnline.Models.AuthentificationModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages.Authentification
{
    public class LoginModel : PageModel
    {
        private readonly DataContext _context;

        [BindProperty]
        public UserLoginModel User { get; set; }

        public LoginModel(DataContext context)
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
                    if (string.IsNullOrEmpty(User.Username))
                    {
                        ModelState.AddModelError(string.Empty, "The username is empty");
                    }
                    else if (string.IsNullOrEmpty(User.Password))
                    {
                        ModelState.AddModelError(string.Empty, "The password is empty");
                    }

                    User? user = _context.User.FirstOrDefault(user => user.username.ToLower() == User.Username.ToLower());
                    if (user != null)
                    {
                        // Verify the entered password against the hashed password stored in the database
                        if (BCrypt.Net.BCrypt.Verify(User.Password, user.password))
                        {
                            // Passwords match, authentication successful
                            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.username!),
                        new Claim("Role", user.role.ToString()),
                        new Claim("UserId", user.user_id.ToString())
                    };
                            var claimIdentity = new ClaimsIdentity(claims, "AuthentificationCookie");

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            // Passwords do not match
                            ModelState.AddModelError(string.Empty, "Invalid username or password!");
                        }
                    }
                    else
                    {
                        // User not found
                        ModelState.AddModelError(string.Empty, "Invalid username or password!");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error logging the account: " + ex.Message);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Authentification/Login");
        }

    }
}
