using CatalogOnline.DAL.DBO;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CatalogOnline.Pages
{
    public class EditGradeModel : PageModel
    {
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly ILogger<EditGradeModel> _logger;

        [BindProperty]
        public Grade Grade { get; set; }
        public List<User> Students { get; set; }
        public List<Course> Courses { get; set; }

        public EditGradeModel(IGradeService gradeService, IUserService studentService, ICourseService courseService, ILogger<EditGradeModel> logger)
        {
            _gradeService = gradeService;
            _userService = studentService;
            _courseService = courseService;
            _logger = logger;
        }

        public IActionResult OnGet(int id)
        {
            Grade = _gradeService.GetGradeById(id);
            if (Grade == null)
            {
                return NotFound();
            }

            Students = _userService.GetAllStudents();
            Courses = _courseService.GetAllCourses();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || !IsValidGrade(Grade))
            {
                Students = _userService.GetAllStudents();
                Courses = _courseService.GetAllCourses();
                return Page();
            }

            var res = _gradeService.UpdateGrade(Grade);
            if (res == null)
            {
                ModelState.AddModelError("EditGradeError", "Failed to update grade");
                Students = _userService.GetAllStudents();
                Courses = _courseService.GetAllCourses();
                return Page();
            }

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
