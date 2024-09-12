using ERPSystem.Models.Document_M;
using ERPSystem.Data.Repository.DocumentM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.DocumentM
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document?> GetDocumentByIdAsync(int id);
        Task CreateDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(int id);
        Task<IEnumerable<Document>> GetDocumentsWithAccessControlsAsync();
        Task<IEnumerable<Document>> GetDocumentsWithVersionsAsync();
    }

    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _documentRepository.GetAllAsync();
        }

        public async Task<Document?> GetDocumentByIdAsync(int id)
        {
            return await _documentRepository.GetByIdAsync(id);
        }

        public async Task CreateDocumentAsync(Document document)
        {
            await _documentRepository.AddAsync(document);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _documentRepository.UpdateAsync(document);
        }

        public async Task DeleteDocumentAsync(int id)
        {
            await _documentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Document>> GetDocumentsWithAccessControlsAsync()
        {
            return await _documentRepository.GetDocumentsWithAccessControlsAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsWithVersionsAsync()
        {
            return await _documentRepository.GetDocumentsWithVersionsAsync();
        }
    }
}
