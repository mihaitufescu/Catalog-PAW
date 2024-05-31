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
        private readonly ILogger<AddEnrollmentModel> _logger;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public AddEnrollmentModel(IEnrollmentService enrollmentService, IUserRepository userRepository, ICourseRepository courseRepository,
            ILogger<AddEnrollmentModel> logger, IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentService = enrollmentService;
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [BindProperty]
        public EnrollmentRegisterModel Enrollment { get; set; }

        public List<User> Students { get; set; }
        public List<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Students = _userRepository.GetAllStudents();
            Courses = _courseRepository.GetAllCourses();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValidEnrollment(Enrollment))
            {
                Students = _userRepository.GetAllStudents();
                Courses = _courseRepository.GetAllCourses();
                return Page();
            }

            var newEnrollment = new Enrollment
            {
                user_id = Enrollment.user_id,
                course_id = Enrollment.course_id,
                semester = Enrollment.semester
            };

            var res = await _enrollmentRepository.AddEnrollment(newEnrollment);
            if (!res)
            {
                ModelState.AddModelError("AddEnrollmentError", "Failed to add enrollment");
                Students = _userRepository.GetAllStudents();
                Courses = _courseRepository.GetAllCourses();
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

            if (enrollment.semester <= 0)
            {
                ModelState.AddModelError("Enrollment.semester", "Semester must be greater than 0.");
                isValid = false;
            }

            return isValid;
        }
    }
}
