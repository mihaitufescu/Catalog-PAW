using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using CatalogOnline.DAL.DBO;

namespace CatalogOnline.Pages
{
    [Authorize(Roles = "student")]
    public class RequestDocumentModel : PageModel
    {
        private readonly IDocumentService _documentService;
        private readonly IUserService _userService;

        public RequestDocumentModel(IDocumentService documentService, IUserService userService)
        {
            _documentService = documentService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostGenerateCertificateAsync()
        {
            var userId = User.FindFirst("UserId")?.Value;//Get the user id from the claims.

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("User ID not found.");
            }

            var user = _userService.GetUserById(int.Parse(userId));

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var pdfContent = GeneratePdf(user);//Generate the pdf.


            var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");//Check root.
            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }

            var pdfFileName = $"Certificate_{user.legal_name}_{DateTime.Now:ddMMyyyy}.pdf";//Name the pdf file.
            var filePath = Path.Combine(documentsPath, pdfFileName);
            System.IO.File.WriteAllBytes(filePath, pdfContent);

            var document = new CatalogOnline.DAL.DBO.Document
            {
                user_id = user.user_id,
                title = "Student Certificate",
                description = "Certificate confirming student status",
                date = DateTime.Now,
                type = "Certificate",
                location = $"/documents/{pdfFileName}" //Path to wwwroot
            };

            _documentService.AddDocument(document);//Add the document to the database.

            return File(pdfContent, "application/pdf", pdfFileName);
        }

        private byte[] GeneratePdf(User user)
        {
           
            var document = new MigraDocCore.DocumentObjectModel.Document();//Create a new document object model for the pdf.
            var section = document.AddSection();
            var paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddFormattedText("Student Certificate", TextFormat.Bold);
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();

            paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText($"This is to certify that {user.legal_name} is currently enrolled as a student in our institution.");
            paragraph.AddLineBreak();
            paragraph.AddText($"Date: {DateTime.Now:dd/MM/yyyy}");

            
            var renderer = new PdfDocumentRenderer();
            renderer.Document = document;
            renderer.RenderDocument();//Actually make it a pdf.

            using (var stream = new MemoryStream())
            {
                renderer.PdfDocument.Save(stream, false);//Save the pdf to a stream.
                return stream.ToArray();//Return the pdf as a byte array.
            }
        }
    }
}
