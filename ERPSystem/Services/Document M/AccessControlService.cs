using ERPSystem.Models.Document_M;
using ERPSystem.Data.Repository.DocumentM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.DocumentM
{
    public interface IAccessControlService
    {
        Task<IEnumerable<AccessControl>> GetAllAccessControlsAsync();
        Task<AccessControl?> GetAccessControlByIdAsync(int id);
        Task CreateAccessControlAsync(AccessControl accessControl);
        Task UpdateAccessControlAsync(AccessControl accessControl);
        Task DeleteAccessControlAsync(int id);
        Task<IEnumerable<AccessControl>> GetAccessControlsByUserIdAsync(int userId);
        Task<IEnumerable<AccessControl>> GetAccessControlsByDocumentIdAsync(int documentId);
    }

    public class AccessControlService : IAccessControlService
    {
        private readonly IAccessControlRepository _accessControlRepository;

        public AccessControlService(IAccessControlRepository accessControlRepository)
        {
            _accessControlRepository = accessControlRepository;
        }

        public async Task<IEnumerable<AccessControl>> GetAllAccessControlsAsync()
        {
            return await _accessControlRepository.GetAllAsync();
        }

        public async Task<AccessControl?> GetAccessControlByIdAsync(int id)
        {
            return await _accessControlRepository.GetByIdAsync(id);
        }

        public async Task CreateAccessControlAsync(AccessControl accessControl)
        {
            await _accessControlRepository.AddAsync(accessControl);
        }

        public async Task UpdateAccessControlAsync(AccessControl accessControl)
        {
            await _accessControlRepository.UpdateAsync(accessControl);
        }

        public async Task DeleteAccessControlAsync(int id)
        {
            await _accessControlRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AccessControl>> GetAccessControlsByUserIdAsync(int userId)
        {
            return await _accessControlRepository.GetAccessControlsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<AccessControl>> GetAccessControlsByDocumentIdAsync(int documentId)
        {
            return await _accessControlRepository.GetAccessControlsByDocumentIdAsync(documentId);
        }
    }
}
