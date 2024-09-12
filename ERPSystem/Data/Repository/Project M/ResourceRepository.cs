using ERPSystem.Models.Project_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.ProjectM
{
    public interface IResourceRepository : IRepository<Resource>
    {
        // Add any additional methods if needed
    }

    public class ResourceRepository : IResourceRepository
    {
        private readonly ERPDbContext _context;

        public ResourceRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return await _context.Resources
                .ToListAsync();
        }

        public async Task<Resource?> GetByIdAsync(int id)
        {
            return await _context.Resources
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Resource entity)
        {
            await _context.Resources.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Resource entity)
        {
            _context.Resources.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Resources.FindAsync(id);
            if (entity != null)
            {
                _context.Resources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
