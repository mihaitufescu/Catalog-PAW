using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "admin")]
    public class EditGradeModel : PageModel
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ILogger<EditGradeModel> logger;

        [BindProperty]
        public Grade Grade { get; set; }
        public List<User> Students { get; set; }
        public List<Course> Courses { get; set; }

        public EditGradeModel(IGradeRepository _gradeRepository, IUserRepository _studentRepository,
            ICourseRepository _courseRepository, ILogger<EditGradeModel> _logger)
        {
            gradeRepository = _gradeRepository;
            userRepository = _studentRepository;
            courseRepository = _courseRepository;
            logger = _logger;
        }

        public IActionResult OnGet(int id)
        {
            Grade = gradeRepository.GetGradeById(id);
            if (Grade == null)
            {
                return NotFound();
            }

            Students = userRepository.GetAllStudents();
            Courses = courseRepository.GetAllCourses();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || !IsValidGrade(Grade))
            {
                Students = userRepository.GetAllStudents();
                Courses = courseRepository.GetAllCourses();
                return Page();
            }

            gradeRepository.UpdateGrade(Grade);

            return RedirectToPage("Grade");
        }

        private bool IsValidGrade(Grade grade)
        {
            bool isValid = true;

            if (grade.score <= 0)
            {
                ModelState.AddModelError("Grade.score", "Score must be greater than 0.");
                isValid = false;
            }

            if (grade.percentage <= 0)
            {
                ModelState.AddModelError("Grade.percentage", "Percentage must be greater than 0.");
                isValid = false;
            }

            return isValid;
        }


    }
}
