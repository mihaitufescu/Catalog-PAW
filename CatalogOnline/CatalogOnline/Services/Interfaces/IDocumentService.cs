using CatalogOnline.DAL.DBO;
using CatalogOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Services.Interfaces
{
    public interface IDocumentService
    {
        List<DocumentModel> GetDocuments();

        Document GetDocumentById(int id);

        Task<bool> AddDocument(Document document);
        Task<bool> UpdateDocument(Document document);
        void DeleteDocument(int id);
    }
}
