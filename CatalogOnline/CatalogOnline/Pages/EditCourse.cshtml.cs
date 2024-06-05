using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "admin")]
    public class EditCourseModel : PageModel
    {
        private readonly ICourseRepository courseRepository;
        private readonly ILogger<EditCourseModel> logger;

        [BindProperty]
        public Course Course { get; set; }

        public EditCourseModel(ILogger<EditCourseModel> _logger, ICourseRepository _courseRepository)
        {
            courseRepository = _courseRepository;
            logger = _logger;
        }

        public IActionResult OnGet(int id)
        {
            Course = courseRepository.GetCourseById(id);
            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IsValidCourse(Course))
            {
                return Page();
            }

            var res = courseRepository.UpdateCourse(Course);
            if (res == null)
            {
                ModelState.AddModelError("UpdateCourseError", "Failed to update course");
                return Page();
            }

            return RedirectToPage("Course");
        }

        private bool IsValidCourse(Course course)
        {
            bool isValid = true;

            if (course.year < 1864)
            {
                ModelState.AddModelError("Course.year", "Year cannot be below 1864.");
                isValid = false;
            }

            if (course.credits_number <= 0)
            {
                ModelState.AddModelError("Course.credits_number", "Credits number must be above 0.");
                isValid = false;
            }

            if (!IsValidSubject(course.subject))
            {
                ModelState.AddModelError("Course.subject", "Subject name must contain only characters.");
                isValid = false;
            }

            return isValid;
        }

        private bool IsValidSubject(string subject)
        {
            // Regular expression to check if the subject contains only letters and spaces
            var regex = new Regex(@"^[a-zA-Z\s]+$");
            return regex.IsMatch(subject);
        }
    }
}
