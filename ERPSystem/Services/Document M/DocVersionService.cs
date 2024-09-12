using ERPSystem.Models.Document_M;
using ERPSystem.Data.Repository.DocumentM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.DocumentM
{
    public interface IDocVersionService
    {
        Task<IEnumerable<DocVersion>> GetAllDocVersionsAsync();
        Task<DocVersion?> GetDocVersionByIdAsync(int id);
        Task CreateDocVersionAsync(DocVersion docVersion);
        Task UpdateDocVersionAsync(DocVersion docVersion);
        Task DeleteDocVersionAsync(int id);
        Task<IEnumerable<DocVersion>> GetDocVersionsWithDocumentsAsync();
    }

    public class DocVersionService : IDocVersionService
    {
        private readonly IDocVersionRepository _docVersionRepository;

        public DocVersionService(IDocVersionRepository docVersionRepository)
        {
            _docVersionRepository = docVersionRepository;
        }

        public async Task<IEnumerable<DocVersion>> GetAllDocVersionsAsync()
        {
            return await _docVersionRepository.GetAllAsync();
        }

        public async Task<DocVersion?> GetDocVersionByIdAsync(int id)
        {
            return await _docVersionRepository.GetByIdAsync(id);
        }

        public async Task CreateDocVersionAsync(DocVersion docVersion)
        {
            await _docVersionRepository.AddAsync(docVersion);
        }

        public async Task UpdateDocVersionAsync(DocVersion docVersion)
        {
            await _docVersionRepository.UpdateAsync(docVersion);
        }

        public async Task DeleteDocVersionAsync(int id)
        {
            await _docVersionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DocVersion>> GetDocVersionsWithDocumentsAsync()
        {
            return await _docVersionRepository.GetDocVersionsWithDocumentsAsync();
        }
    }
}
