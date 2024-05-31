using CatalogOnline.Services.Interfaces;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.Pages
{
    public class ViewCoursesModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IEnrollmentRepository enrollmentRepository;

        private readonly ILogger<ViewCoursesModel> _logger;

        public ViewCoursesModel(IEnrollmentService enrollmentService,
            ILogger<ViewCoursesModel> logger, IEnrollmentRepository _enrollmentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _enrollmentService = enrollmentService;
            enrollmentRepository = _enrollmentRepository;
            _logger = logger;

            httpContextAccessor.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            httpContextAccessor.HttpContext.Response.Headers["Pragma"] = "no-cache";
            httpContextAccessor.HttpContext.Response.Headers["Expires"] = "0";
        }

        public List<StudentCourseModel> Courses { get; private set; }
        public int? SelectedYear { get; set; }
        public string SortOrder { get; set; }
        public List<SelectListItem> Years { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Year 1" },
            new SelectListItem { Value = "2", Text = "Year 2" },
            new SelectListItem { Value = "3", Text = "Year 3" },
            new SelectListItem { Value = "4", Text = "Year 4" },
        };

        public async Task OnGetAsync(int? year, string sortOrder)
        {
            HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";//fixed a bug where the table rows were not updating
            HttpContext.Response.Headers["Pragma"] = "no-cache";
            HttpContext.Response.Headers["Expires"] = "0";

            SelectedYear = year;
            SortOrder = sortOrder;
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                if (SelectedYear.HasValue)
                {
                    double totalMedie = 0;
                    int courseCount = 0;
                    int maxTotalMedie = 0;

                    Courses = await _enrollmentService.GetCoursesByYearAsync(userId, SelectedYear.Value);
                    foreach (var course in Courses)
                    {
                        course.Semester = enrollmentRepository.GetEnrollmentByUserAndCourseID(userId, course.CourseId).semester;
                        course.CalculateMedie();
                        totalMedie += course.Medie;
                        maxTotalMedie += 10;
                        courseCount++;
                    }
                    ViewData["AverageMedie"] = totalMedie / maxTotalMedie * 10;
                    ViewData["MaxTotal"] = maxTotalMedie;

                    switch (SortOrder)
                    {
                        case "name":
                            Courses = Courses.OrderBy(c => c.CourseName).ToList();
                            break;
                        case "medie":
                            Courses = Courses.OrderByDescending(c => c.Medie).ToList();
                            break;
                    }
                }
                else
                {
                    Courses = await _enrollmentService.GetAllCoursesAsync(userId);

                    switch (SortOrder)
                    {
                        case "name":
                            Courses = Courses.OrderBy(c => c.CourseName).ToList();
                            break;
                        case "medie":
                            Courses = Courses.OrderByDescending(c => c.Medie).ToList();
                            break;
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User ID is not valid.");
            }
        }
    }
}
