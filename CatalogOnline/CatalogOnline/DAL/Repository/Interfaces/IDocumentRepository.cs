using CatalogOnline.DAL.DBO;
using System.Reflection.Metadata;
using Document = CatalogOnline.DAL.DBO.Document;

namespace CatalogOnline.DAL.Repository.Interfaces
{
    public interface IDocumentRepository
    {

        public List<Document> GetDocuments();

        public Document GetDocument(int id);
        
    }
}
