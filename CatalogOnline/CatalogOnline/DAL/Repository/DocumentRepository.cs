using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.DAL.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        DataContext _context;
        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public List<Document> GetDocuments()
        {
            return _context.Document.OrderBy(d => d.document_id).ToList();
        }

        public Document GetDocument(int id)
        {
            return _context.Document.FirstOrDefault(d => d.document_id == id);
        }

    }
}
