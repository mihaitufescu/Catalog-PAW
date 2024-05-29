using CatalogOnline.DAL.DBO;
using CatalogOnline.Services;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CatalogOnline.Pages
{
    public class EditCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<EditCourseModel> _logger;

        [BindProperty]
        public Course Course { get; set; }

        public EditCourseModel(ICourseService courseService, ILogger<EditCourseModel> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        public IActionResult OnGet(int id)
        {
            Course = _courseService.GetCourseById(id);
            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var res = _courseService.UpdateCourse(Course);
            if (res == null)
            {
                ModelState.AddModelError("UpdateCourseError", "Failed to update course");
                return Page();
            }

            return RedirectToPage("Course");
        }
    }
}
