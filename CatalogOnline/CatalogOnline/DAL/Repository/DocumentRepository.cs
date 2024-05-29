using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogOnline.DAL.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public List<Document> GetDocuments()
        {
            return _context.Document.OrderBy(d => d.document_id).ToList();
        }

        public Document GetDocumentById(int id)
        {
            return _context.Document.Find(id);
        }

        public async Task<bool> AddDocument(Document document)
        {
            _context.Document.Add(document);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateDocument(Document document)
        {
            var existingDocument = await _context.Document.FindAsync(document.document_id);
            if (existingDocument != null)
            {
                _context.Entry(existingDocument).State = EntityState.Detached;
                _context.Document.Update(document);
                var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            return false;
        }

        public void DeleteDocument(int id)
        {

            var document = _context.Document.Find(id);
            if (document != null)
            {
                _context.Document.Remove(document);
                _context.SaveChanges();
            }

        }


    }
}
