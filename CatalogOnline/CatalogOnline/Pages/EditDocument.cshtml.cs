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
    public class EditDocumentModel : PageModel
    {
        private readonly ILogger<EditDocumentModel> logger;
        private readonly IDocumentRepository documentRepository;
        private readonly IUserRepository userRepository;

        public EditDocumentModel(IDocumentRepository _documentRepository, IUserRepository _userRepository, ILogger<EditDocumentModel> _logger)
        {
            documentRepository = _documentRepository;
            userRepository = _userRepository;
            logger = _logger;
        }

        [BindProperty]
        public required DocumentRegisterModel Document { get; set; }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var document = documentRepository.GetDocumentById(id);
            if (document == null)
            {
                return NotFound();
            }

            Document = new DocumentRegisterModel
            {
                document_id = document.document_id,
                user_id = document.user_id,
                title = document.title,
                description = document.description,
                date = document.date,
                type = document.type,
                location = document.location
            };

            Users = userRepository.GetAllUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !await IsValidDocument(Document))
            {
                Users = userRepository.GetAllUsers();
                return Page();
            }

            var updatedDocument = new Document
            {
                document_id = Document.document_id,
                user_id = Document.user_id,
                title = Document.title,
                description = Document.description,
                date = Document.date,
                type = Document.type,
                location = Document.location
            };

            var res =  documentRepository.UpdateDocument(updatedDocument);
            if (res is null)
            {
                ModelState.AddModelError("EditDocumentError", "Failed to update document");
                Users = userRepository.GetAllUsers();
                return Page();
            }

            return RedirectToPage("Document");
        }

        private async Task<bool> IsValidDocument(DocumentRegisterModel document)
        {
            bool isValid = true;

            if (document.user_id <= 0 || !await userRepository.UserExists(document.user_id))
            {
                ModelState.AddModelError("Document.user_id", "User ID must be greater than 0 and must exist.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(document.title))
            {
                ModelState.AddModelError("Document.title", "Title is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(document.description))
            {
                ModelState.AddModelError("Document.description", "Description is required.");
                isValid = false;
            }

            if (document.date == DateTime.MinValue)
            {
                ModelState.AddModelError("Document.date", "Date is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(document.type))
            {
                ModelState.AddModelError("Document.type", "Type is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(document.location))
            {
                ModelState.AddModelError("Document.location", "Location is required.");
                isValid = false;
            }

            return isValid;
        }
    }
}
