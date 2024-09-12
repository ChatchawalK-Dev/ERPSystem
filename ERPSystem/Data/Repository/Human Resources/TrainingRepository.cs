using ERPSystem.Models.HumanResources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.HumanResources
{
    public interface ITrainingRepository : IRepository<Training>
    {
        Task<IEnumerable<Training>> GetTrainingsByEmployeeIdAsync(int employeeId);
    }

    public class TrainingRepository : ITrainingRepository
    {
        private readonly ERPDbContext _context;

        public TrainingRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return await _context.Trainings
                .Include(t => t.Employee)
                .ToListAsync();
        }

        public async Task<Training?> GetByIdAsync(int id)
        {
            return await _context.Trainings
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Training entity)
        {
            await _context.Trainings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Training entity)
        {
            _context.Trainings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Trainings.FindAsync(id);
            if (entity != null)
            {
                _context.Trainings.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Training>> GetTrainingsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Trainings
                .Where(t => t.EmployeeID == employeeId)
                .Include(t => t.Employee)
                .ToListAsync();
        }
    }
}
