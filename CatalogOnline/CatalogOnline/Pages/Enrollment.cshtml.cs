using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.Pages
{
    public class EnrollmentPageModel : PageModel
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly IEnrollmentRepository enrollmentRepository;


        public EnrollmentPageModel(IEnrollmentService _enrollmentService, IEnrollmentRepository _enrollmentRepository)
        {
            enrollmentService = _enrollmentService;
            enrollmentRepository = _enrollmentRepository;
        }

        public List<EnrollmentModel> Enrollments { get; private set; }

        public void OnGet()
        {
            Enrollments = enrollmentService.GetAllEnrollments();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            enrollmentRepository.DeleteEnrollment(id);
            return RedirectToPage();
        }
    }
}
