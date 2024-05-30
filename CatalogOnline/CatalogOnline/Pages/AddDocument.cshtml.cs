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
    public class AddDocumentModel : PageModel
    {
        private readonly ILogger<AddDocumentModel> logger;
        private readonly IDocumentRepository documentRepository;
        private readonly IUserRepository userRepository;

        public AddDocumentModel(IDocumentRepository _documentRepository, IUserRepository _userRepository, ILogger<AddDocumentModel> _logger)
        {
            documentRepository = _documentRepository;
            userRepository = _userRepository;
            _logger = _logger;
        }

        [BindProperty]
        public required DocumentRegisterModel Document { get; set; }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = userRepository.GetAllStudents();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !await IsValidDocument(Document))
            {
                Users = userRepository.GetAllUsers();
                return Page();
            }

            var newDocument = new Document
            {
                user_id = Document.user_id,
                title = Document.title,
                description = Document.description,
                date = Document.date,
                type = Document.type,
                location = Document.location
            };

            var res = await documentRepository.AddDocument(newDocument);
            if (!res)
            {
                ModelState.AddModelError("AddDocumentError", "Failed to add document");
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
