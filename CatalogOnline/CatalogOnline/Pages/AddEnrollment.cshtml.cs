using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class AddEnrollmentModel : PageModel
    {
        private readonly ILogger<AddEnrollmentModel> _logger;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public AddEnrollmentModel(IEnrollmentService enrollmentService, IUserService userService, ICourseService courseService, ILogger<AddEnrollmentModel> logger)
        {
            _enrollmentService = enrollmentService;
            _userService = userService;
            _courseService = courseService;
            _logger = logger;
        }

        [BindProperty]
        public required EnrollmentRegisterModel Enrollment { get; set; }

        public List<User> Students { get; set; }
        public List<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Students = _userService.GetAllStudents();
            Courses = _courseService.GetAllCourses();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValidEnrollment(Enrollment))
            {
                Students = _userService.GetAllStudents();
                Courses = _courseService.GetAllCourses();
                return Page();
            }

            var newEnrollment = new Enrollment
            {
                user_id = Enrollment.user_id,
                course_id = Enrollment.course_id,
                joined_since = Enrollment.joined_since
            };

            var res = _enrollmentService.AddEnrollment(newEnrollment);
            if (!res)
            {
                ModelState.AddModelError("AddEnrollmentError", "Failed to add enrollment");
                Students = _userService.GetAllStudents();
                Courses = _courseService.GetAllCourses();
                return Page();
            }

            return RedirectToPage("Enrollment");
        }

        private bool IsValidEnrollment(EnrollmentRegisterModel enrollment)
        {
            bool isValid = true;

            if (enrollment.user_id <= 0)
            {
                ModelState.AddModelError("Enrollment.user_id", "User ID must be greater than 0.");
                isValid = false;
            }

            if (enrollment.course_id <= 0)
            {
                ModelState.AddModelError("Enrollment.course_id", "Course ID must be greater than 0.");
                isValid = false;
            }

            if (enrollment.joined_since == DateTime.MinValue)
            {
                ModelState.AddModelError("Enrollment.joined_since", "Joined Since date is required.");
                isValid = false;
            }

            return isValid;
        }
    }
}
