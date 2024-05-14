using AutoMapper;
using CatalogOnline.DAL.Repository.Interfaces;
using CatalogOnline.Models;
using CatalogOnline.Services.Interfaces;

namespace CatalogOnline.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepository documentRepository;

        public DocumentService(IMapper mapper, IDocumentRepository documentRepository)
        {
            _mapper = mapper;
            this.documentRepository = documentRepository;
        }

        public List<DocumentModel> GetDocuments()
        {
            var documents = documentRepository.GetDocuments();
            return _mapper.Map<List<DocumentModel>>(documents);
        }
    }
}
