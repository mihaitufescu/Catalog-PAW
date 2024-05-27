using CatalogOnline.DAL.DBO;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class AddUserModel : PageModel
    {
        private readonly IUserService _usersService;

        public AddUserModel(IUserService usersService)
        {
            _usersService = usersService;
        }

        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _usersService.AddUserAsync(NewUser);
            return RedirectToPage("User"); // Corrected redirect
        }
    }
}
