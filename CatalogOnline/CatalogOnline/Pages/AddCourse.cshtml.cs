using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using CatalogOnline.DAL.Repository.Interfaces;
namespace CatalogOnline.Pages
{
    public class AddCourseModel : PageModel
    {
        private readonly ICourseRepository courseRepository;
 
        private readonly ILogger<AddCourseModel> logger;


        public AddCourseModel(ICourseRepository _courseRepository, ILogger<AddCourseModel> _logger)
        {
            courseRepository = _courseRepository;
            logger = _logger;
        }

        public void OnGet()
        {
        }
        [BindProperty]
        public required CourseRegisterModel Course { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create a new course entity
            var newCourse = new Course
            {
                subject = Course.Subject,
                mandatory = Course.Mandatory,
                credits_number = Course.CreditsNumber,
                year = Course.Year
            };
            var res = courseRepository.AddCourse(newCourse);
            if (res is null)
            {
                ModelState.AddModelError("AddCourseError", "Failed to add course");
                return Page(); // Return the same page to display the error
            }

            return RedirectToPage("Course"); // Corrected redirect
        }   
    }
}
