using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class GradePageModel : PageModel
    {
        private readonly IGradeService _gradeService;
        public List<GradeModel> Grades { get; private set; }
        public GradePageModel(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        public void OnGet()
        {
            Grades = _gradeService.GetAllGrades();
        }
    }
}
