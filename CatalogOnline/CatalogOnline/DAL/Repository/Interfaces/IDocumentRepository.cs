using CatalogOnline.DAL.DBO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        List<Document> GetDocuments();

        Document GetDocumentById(int id);
        Task<bool> AddDocument(Document document);
        Task<bool> UpdateDocument(Document document);
        void DeleteDocument(int id);

        IEnumerable<Document> GetDocumentsByUserId(int userId);

    }
}
