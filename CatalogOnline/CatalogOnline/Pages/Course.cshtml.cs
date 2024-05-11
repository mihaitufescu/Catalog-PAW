using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;

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
            Courses = _courseService.GetAllCourses(); 
        }
    }
}
