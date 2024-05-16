using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models.AuthentificationModels
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Legal name is required")]
        public string? LegalName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }
}
