using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "admin,secretary")]
    public class EditEnrollmentModel : PageModel
    {
        private readonly ILogger<EditEnrollmentModel> logger;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;

        public EditEnrollmentModel(IEnrollmentRepository _enrollmentRepository, IUserRepository _userRepository,
            ICourseRepository _courseRepository, ILogger<EditEnrollmentModel> _logger)
        {
            enrollmentRepository = _enrollmentRepository;
            userRepository = _userRepository;
            courseRepository = _courseRepository;
            logger = _logger;
        }

        [BindProperty]
        public required EnrollmentRegisterModel Enrollment { get; set; }

        public List<User> Students { get; set; }
        public List<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var enrollment = enrollmentRepository.GetEnrollmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            Enrollment = new EnrollmentRegisterModel
            {
                enrollment_id = enrollment.enrollment_id,
                user_id = enrollment.user_id,
                course_id = enrollment.course_id,
                semester = enrollment.semester
            };

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

            var updatedEnrollment = new Enrollment
            {
                enrollment_id = Enrollment.enrollment_id,
                user_id = Enrollment.user_id,
                course_id = Enrollment.course_id,
                semester = Enrollment.semester
            };

            var res =  enrollmentRepository.UpdateEnrollment(updatedEnrollment);
            if (res is null)
            {
                ModelState.AddModelError("EditEnrollmentError", "Failed to update enrollment");
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

            if (enrollment.semester == 0)
            {
                ModelState.AddModelError("Enrollment.joined_since", "Joined Since date is required.");
                isValid = false;
            }

            return isValid;
        }
    }
}
