using AutoMapper;
using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogOnline.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IMapper mapper, IDocumentRepository documentRepository)
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
        }

        public List<DocumentModel> GetDocuments()
        {
            var documents = _documentRepository.GetDocuments();
            return _mapper.Map<List<DocumentModel>>(documents);
        }

        public async Task<bool> AddDocument(Document document)
        {
            return await _documentRepository.AddDocument(document);
        }

        public async Task<bool> UpdateDocument(Document document)
        {
            return await _documentRepository.UpdateDocument(document);
        }

        public void DeleteDocument(int id)
        {
             _documentRepository.DeleteDocument(id);
        }

        public Document GetDocumentById(int id)
        {
            return _documentRepository.GetDocumentById(id);
        }
    }
}
