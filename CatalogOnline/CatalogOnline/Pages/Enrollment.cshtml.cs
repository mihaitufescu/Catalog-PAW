using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;

namespace CatalogOnline.Pages
{
    public class EnrollmentPageModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentPageModel(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public List<EnrollmentModel> Enrollments { get; private set; }

        public void OnGet()
        {
            Enrollments = _enrollmentService.GetAllEnrollments();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _enrollmentService.DeleteEnrollment(id);
            return RedirectToPage();
        }
    }
}
