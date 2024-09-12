using ERPSystem.Models.Project_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.ProjectM
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllProjectsWithTasksAndAllocationsAsync();
    }

    public class ProjectRepository : IProjectRepository
    {
        private readonly ERPDbContext _context;

        public ProjectRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectTasks) // ใช้ ProjectTasks แทน Task
                .Include(p => p.ResourceAllocations)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.ProjectTasks) // ใช้ ProjectTasks แทน Task
                .Include(p => p.ResourceAllocations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Project entity)
        {
            await _context.Projects.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project entity)
        {
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Projects.FindAsync(id);
            if (entity != null)
            {
                _context.Projects.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> GetAllProjectsWithTasksAndAllocationsAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectTasks) // ใช้ ProjectTasks แทน Task
                .Include(p => p.ResourceAllocations)
                .ToListAsync();
        }
    }
}
