using ERPSystem.Models.Project_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.ProjectM
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
    }

    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ERPDbContext _context;

        public ProjectTaskRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllAsync()
        {
            return await _context.ProjectTasks
                .ToListAsync();
        }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _context.ProjectTasks
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(ProjectTask entity)
        {
            await _context.ProjectTasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTask entity)
        {
            _context.ProjectTasks.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProjectTasks.FindAsync(id);
            if (entity != null)
            {
                _context.ProjectTasks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Where(t => t.ProjectID == projectId)
                .ToListAsync();
        }
    }
}
