using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using CatalogOnline.Services;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.Pages
{
    public class CoursePageModel : PageModel 
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICourseService courseService;

        public CoursePageModel(ICourseRepository _courseRepository, ICourseService _courseService) 
        {
            courseRepository = _courseRepository;
            courseService = _courseService;
        }

        public List<CourseModel> Courses { get; private set; }

        public void OnGet()
        {
            Courses = courseService.GetAllCourseModels(); 
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            courseRepository.DeleteCourse(id);
            return RedirectToPage();
        }
    }
}
