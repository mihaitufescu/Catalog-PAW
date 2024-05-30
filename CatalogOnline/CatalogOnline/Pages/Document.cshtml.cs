using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogOnline.Pages
{
    public class DocumentPageModel : PageModel
    {
        private readonly IDocumentService documentService;

        private readonly IDocumentRepository documentRepository;

        public List<DocumentModel> Documents { get; private set; }

        public DocumentPageModel(IDocumentService _documentService, IDocumentRepository _documentRepository)
        {
            documentService = _documentService;
            documentRepository = _documentRepository;
        }

        public void OnGet()
        {
            Documents = documentService.GetDocuments();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            documentRepository.DeleteDocument(id);
            return RedirectToPage();
        }
    }
}
