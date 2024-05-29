using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Pages
{
    public class DocumentPageModel : PageModel
    {
        private readonly IDocumentService _documentService;

        public List<DocumentModel> Documents { get; private set; }

        public DocumentPageModel(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public void OnGet()
        {
            Documents = _documentService.GetDocuments();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _documentService.DeleteDocument(id);
            return RedirectToPage();
        }
    }
}
