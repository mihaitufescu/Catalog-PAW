using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using CatalogOnline.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CatalogOnline.Pages
{
    public class ViewCoursesModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;

        public ViewCoursesModel(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public List<StudentCourseModel> Courses { get; private set; }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("student"))
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    int userId = int.Parse(userIdClaim.Value);
                    Courses = _enrollmentService.GetStudentCoursesWithGrades(userId);
                }
            }
        }
    }
}
