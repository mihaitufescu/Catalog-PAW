using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class GradePageModel : PageModel
    {
        private readonly IGradeService gradeService;

        private readonly IGradeRepository gradeRepository;

        public List<GradeModel> Grades { get; private set; }

        public GradePageModel(IGradeService _gradeService, IGradeRepository _gradeRepository)
        {
            gradeService = _gradeService;

            gradeRepository = _gradeRepository;
        }

        public void OnGet()
        {
            Grades = gradeService.GetAllGrades();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            gradeRepository.DeleteGrade(id);
            return RedirectToPage();
        }
    }
}
