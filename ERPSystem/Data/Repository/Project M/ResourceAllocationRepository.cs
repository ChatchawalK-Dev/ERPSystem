using ERPSystem.Models.Project_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.ProjectM
{
    public interface IResourceAllocationRepository : IRepository<ResourceAllocation>
    {
        Task<IEnumerable<ResourceAllocation>> GetAllocationsByProjectIdAsync(int projectId);
        Task<IEnumerable<ResourceAllocation>> GetAllocationsByResourceIdAsync(int resourceId);
    }

    public class ResourceAllocationRepository : IResourceAllocationRepository
    {
        private readonly ERPDbContext _context;

        public ResourceAllocationRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResourceAllocation>> GetAllAsync()
        {
            return await _context.ResourceAllocations
                .Include(r => r.Project)
                .Include(r => r.Resource)
                .ToListAsync();
        }

        public async Task<ResourceAllocation?> GetByIdAsync(int id)
        {
            return await _context.ResourceAllocations
                .Include(r => r.Project)
                .Include(r => r.Resource)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(ResourceAllocation entity)
        {
            await _context.ResourceAllocations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ResourceAllocation entity)
        {
            _context.ResourceAllocations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ResourceAllocations.FindAsync(id);
            if (entity != null)
            {
                _context.ResourceAllocations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ResourceAllocation>> GetAllocationsByProjectIdAsync(int projectId)
        {
            return await _context.ResourceAllocations
                .Where(r => r.ProjectID == projectId)
                .Include(r => r.Project)
                .Include(r => r.Resource)
                .ToListAsync();
        }

        public async Task<IEnumerable<ResourceAllocation>> GetAllocationsByResourceIdAsync(int resourceId)
        {
            return await _context.ResourceAllocations
                .Where(r => r.ResourceID == resourceId)
                .Include(r => r.Project)
                .Include(r => r.Resource)
                .ToListAsync();
        }
    }
}
