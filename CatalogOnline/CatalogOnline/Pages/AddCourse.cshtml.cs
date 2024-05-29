using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
namespace CatalogOnline.Pages
{
    public class AddCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<AddCourseModel> _logger;


        public AddCourseModel(ICourseService courseService, ILogger<AddCourseModel> logger)
        {
            _courseService = courseService;
            _logger = logger;
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
            var res = await _courseService.AddCourse(newCourse);
            if (!res)
            {
                ModelState.AddModelError("AddCourseError", "Failed to add course");
                return Page(); // Return the same page to display the error
            }

            return RedirectToPage("Course"); // Corrected redirect
        }   
    }
}
