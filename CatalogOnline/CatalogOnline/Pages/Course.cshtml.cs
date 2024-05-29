using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using CatalogOnline.Services;

namespace CatalogOnline.Pages
{
    public class CoursePageModel : PageModel 
    {
        private readonly ICourseService _courseService;

        public CoursePageModel(ICourseService courseService) 
        {
            _courseService = courseService;
        }

        public List<CourseModel> Courses { get; private set; }

        public void OnGet()
        {
            Courses = _courseService.GetAllCourseModels(); 
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _courseService.DeleteCourse(id);
            return RedirectToPage();
        }
    }
}
