using ERPSystem.Models.Project_M;
using ERPSystem.Data.Repository.ProjectM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.ProjectM
{
    public interface IResourceAllocationService
    {
        Task<IEnumerable<ResourceAllocation>> GetAllAllocationsAsync();
        Task<ResourceAllocation?> GetAllocationByIdAsync(int id);
        Task CreateAllocationAsync(ResourceAllocation resourceAllocation);
        Task UpdateAllocationAsync(ResourceAllocation resourceAllocation);
        Task DeleteAllocationAsync(int id);
        Task<IEnumerable<ResourceAllocation>> GetAllocationsByProjectIdAsync(int projectId);
        Task<IEnumerable<ResourceAllocation>> GetAllocationsByResourceIdAsync(int resourceId);
    }

    public class ResourceAllocationService : IResourceAllocationService
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;

        public ResourceAllocationService(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<IEnumerable<ResourceAllocation>> GetAllAllocationsAsync()
        {
            return await _resourceAllocationRepository.GetAllAsync();
        }

        public async Task<ResourceAllocation?> GetAllocationByIdAsync(int id)
        {
            return await _resourceAllocationRepository.GetByIdAsync(id);
        }

        public async Task CreateAllocationAsync(ResourceAllocation resourceAllocation)
        {
            await _resourceAllocationRepository.AddAsync(resourceAllocation);
        }

        public async Task UpdateAllocationAsync(ResourceAllocation resourceAllocation)
        {
            await _resourceAllocationRepository.UpdateAsync(resourceAllocation);
        }

        public async Task DeleteAllocationAsync(int id)
        {
            await _resourceAllocationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResourceAllocation>> GetAllocationsByProjectIdAsync(int projectId)
        {
            return await _resourceAllocationRepository.GetAllocationsByProjectIdAsync(projectId);
        }

        public async Task<IEnumerable<ResourceAllocation>> GetAllocationsByResourceIdAsync(int resourceId)
        {
            return await _resourceAllocationRepository.GetAllocationsByResourceIdAsync(resourceId);
        }
    }
}
