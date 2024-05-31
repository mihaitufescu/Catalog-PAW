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

        [BindProperty]
        public CourseRegisterModel Course { get; set; } = new CourseRegisterModel();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newCourse = new Course
            {
                subject = Course.Subject,
                mandatory = Course.Mandatory,
                credits_number = Course.CreditsNumber,
                year = Course.Year
            };

            var result = await courseRepository.AddCourse(newCourse);
            if (!result)
            {
                ModelState.AddModelError("AddCourseError", "Failed to add course");
                return Page();
            }

            return RedirectToPage("Course");
        }
    }
}
