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
    public class AddGradeModel : PageModel
    {
        private readonly ILogger<AddGradeModel> _logger;
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public AddGradeModel(IGradeService gradeService, IUserService userService, ICourseService courseService, ILogger<AddGradeModel> logger)
        {
            _gradeService = gradeService;
            _userService = userService;
            _courseService = courseService;
            _logger = logger;
        }

        [BindProperty]
        public required GradeRegisterModel Grade { get; set; }

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
            if (!ModelState.IsValid || !IsValidGrade(Grade))
            {
                Students = _userService.GetAllStudents();
                Courses = _courseService.GetAllCourses();
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

            var res = await _gradeService.AddGrade(newGrade);
            if (!res)
            {
                ModelState.AddModelError("AddGradeError", "Failed to add grade");
                Students = _userService.GetAllStudents();
                Courses = _courseService.GetAllCourses();
                return Page();
            }

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
