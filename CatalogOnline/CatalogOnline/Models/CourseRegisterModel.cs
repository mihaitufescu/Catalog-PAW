using System.ComponentModel.DataAnnotations;

namespace CatalogOnline.Models
{
    public class CourseRegisterModel
    {
        [Required(ErrorMessage = "Subject is required")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Mandatory is required")]
        public bool Mandatory { get; set; }

        [Required(ErrorMessage = "Credits number is required")]
        public int CreditsNumber { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }


    }
}
