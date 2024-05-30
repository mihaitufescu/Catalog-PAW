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
    public class AddGradeModel : PageModel
    {
        private readonly ILogger<AddGradeModel> logger;
        private readonly IGradeRepository gradeRepository;
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;

        public AddGradeModel(IGradeRepository _gradeRepository, IUserRepository _userRepository,
            ICourseRepository _courseRepository, ILogger<AddGradeModel> _logger)
        {
            gradeRepository = _gradeRepository;
            userRepository = _userRepository;
            courseRepository = _courseRepository;
            logger = _logger;
        }

        [BindProperty]
        public required GradeRegisterModel Grade { get; set; }

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
            if (!ModelState.IsValid || !IsValidGrade(Grade))
            {
                Students = userRepository.GetAllStudents();
                Courses = courseRepository.GetAllCourses();
                return Page();
            }

            var newGrade = new Grade
            {
                user_id = Grade.user_id,
                course_id = Grade.course_id,
                score = Grade.score,
                type_of_exam = Grade.type_of_exam,
                percentage = Grade.percentage
            };



            return RedirectToPage("Grade");
        }

        private bool IsValidGrade(GradeRegisterModel grade)
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
