using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
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
        private readonly ILogger<AddEnrollmentModel> logger;
        private readonly IEnrollmentService enrollmentService;
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public AddEnrollmentModel(IEnrollmentService _enrollmentService, IUserRepository _userRepository, ICourseRepository _courseRepository,
            ILogger<AddEnrollmentModel> _logger, IEnrollmentRepository _enrollmentRepository)
        {
            enrollmentService = _enrollmentService;
            enrollmentRepository = _enrollmentRepository;
            userRepository = _userRepository;
            courseRepository = _courseRepository;
            logger = _logger;
        }

        [BindProperty]
        public required EnrollmentRegisterModel Enrollment { get; set; }

        public List<User> Students { get; set; }
        public List<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Students = userRepository.GetAllStudents();
            Courses = courseRepository.GetAllCourses();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValidEnrollment(Enrollment))
            {
                Students = userRepository.GetAllStudents();
                Courses = courseRepository.GetAllCourses();
                return Page();
            }

            var newEnrollment = new Enrollment
            {
                user_id = Enrollment.user_id,
                course_id = Enrollment.course_id,
                joined_since = Enrollment.joined_since
            };

            var res = enrollmentRepository.AddEnrollment(newEnrollment);
            if (res is null)
            {
                ModelState.AddModelError("AddEnrollmentError", "Failed to add enrollment");
                Students = userRepository.GetAllStudents();
                Courses = courseRepository.GetAllCourses();
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
