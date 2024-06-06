using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class ViewCourseDetailsModel : PageModel
    {
        private readonly ICourseRepository courseRepository;

        public ViewCourseDetailsModel(ICourseRepository _courseRepository)
        {
            courseRepository = _courseRepository;
        }

        public Course Course { get; set; }

        public void OnGet(int id)
        {
            Course = courseRepository.GetCourseById(id);
        }
    }
}
