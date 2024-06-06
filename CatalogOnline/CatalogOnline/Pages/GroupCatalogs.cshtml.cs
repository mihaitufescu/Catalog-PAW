using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "admin")]
    public class GroupCatalogsModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly IGradeRepository gradeRepository;
        private readonly ICourseRepository courseRepository;

        public GroupCatalogsModel(IUserRepository _userRepository, IGradeRepository _gradeRepository, ICourseRepository courseRepository)
        {
            userRepository = _userRepository;
            gradeRepository = _gradeRepository;
            this.courseRepository = courseRepository;
        }

        public List<string> Groups { get; set; }

        public void OnGet()
        {
            Groups = userRepository.GetAllStudents()
                .Select(u => u.group)
                .Distinct()
                .Where(g => !string.IsNullOrEmpty(g))
                .ToList();
        }

        public async Task<IActionResult> OnPostGenerateGroupReportAsync(string group)
        {
            var students = userRepository.GetAllStudents()
                .Where(u => u.group == group)
                .ToList();

            var pdfContent = GenerateGroupPdf(group, students);

            var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }

            var pdfFileName = $"GroupReport_{group}_{DateTime.Now:ddMMyyyy}.pdf";
            var filePath = Path.Combine(documentsPath, pdfFileName);
            System.IO.File.WriteAllBytes(filePath, pdfContent);

            return File(pdfContent, "application/pdf", pdfFileName);
        }

        private byte[] GenerateGroupPdf(string group, List<User> students)
        {
            var document = new MigraDocCore.DocumentObjectModel.Document();
            var section = document.AddSection();
            var paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddFormattedText($"Group Report: {group}", TextFormat.Bold);
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();

            foreach (var student in students)
            {
                paragraph = section.AddParagraph();
                paragraph.Format.Alignment = ParagraphAlignment.Left;
                paragraph.AddFormattedText($"{student.legal_name} ({student.username})", TextFormat.Bold);
                paragraph.AddLineBreak();

                var grades = gradeRepository.GetGradesByUserId(student.user_id);
                foreach (var grade in grades)
                {
                    Course course = courseRepository.GetCourseById(grade.course_id);
                    paragraph.AddText($"Course: {course.subject}, Score: {grade.score}, Type: {grade.type_of_exam}");
                    paragraph.AddLineBreak();
                }
                paragraph.AddLineBreak();
            }

            var renderer = new PdfDocumentRenderer();
            renderer.Document = document;
            renderer.RenderDocument();

            using (var stream = new MemoryStream())
            {
                renderer.PdfDocument.Save(stream, false);
                return stream.ToArray();
            }
        }
    }
}
