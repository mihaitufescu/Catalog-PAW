using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "student")]
    public class GradePageModel : PageModel
    {
        private readonly IGradeService gradeService;

        private readonly IGradeRepository gradeRepository;

        public List<DisplayGradeModel> Grades { get; private set; }

        public GradePageModel(IGradeService _gradeService, IGradeRepository _gradeRepository)
        {
            gradeService = _gradeService;

            gradeRepository = _gradeRepository;
        }

        public IActionResult OnGet()
        {
            // Get the student ID from claims
            var studentIdClaim = User.FindFirst("UserId");
            if (studentIdClaim != null && int.TryParse(studentIdClaim.Value, out int studentId))
            {
                // Use the student ID to fetch the grades
                Grades = gradeService.GetGradesWithCourseDetailsByStudentId(studentId);
                return Page();
            }
            else
            {
                // Redirect the user to the login page with an error message
                return RedirectToPage("/Authentification/Login", new { error = "Something went wrong with the session." });
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            gradeRepository.DeleteGrade(id);
            return RedirectToPage();
        }
    }
}
