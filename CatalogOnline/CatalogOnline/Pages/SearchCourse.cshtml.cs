using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class SearchCourseModel : PageModel
    {
        private readonly ICourseRepository courseRepository;

        public SearchCourseModel(ICourseRepository _courseRepository)
        {
            courseRepository = _courseRepository;
        }

        public async Task<IActionResult> OnGetAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToPage("/Index");
            }

            var course = courseRepository.FindCourseByName(searchTerm);

            if (course != null)
            {
                return RedirectToPage("/ViewCourseDetails", new { id = course.course_id });
            }

            ModelState.AddModelError(string.Empty, "Course not found");
            return Page();
        }
    }
}
