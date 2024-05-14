using CatalogOnline.DAL.DBO;  
using CatalogOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;

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
    }
}