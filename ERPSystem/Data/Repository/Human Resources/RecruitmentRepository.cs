using ERPSystem.Models.HumanResources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.HumanResources
{
    public interface IRecruitmentRepository : IRepository<Recruitment>
    {
        Task<IEnumerable<Recruitment>> GetRecruitmentsByStatusAsync(string status);
    }

    public class RecruitmentRepository : IRecruitmentRepository
    {
        private readonly ERPDbContext _context;

        public RecruitmentRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recruitment>> GetAllAsync()
        {
            return await _context.Recruitments
                .Include(r => r.Employees)
                .ToListAsync();
        }

        public async Task<Recruitment?> GetByIdAsync(int id)
        {
            return await _context.Recruitments
                .Include(r => r.Employees)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Recruitment entity)
        {
            await _context.Recruitments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recruitment entity)
        {
            _context.Recruitments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Recruitments.FindAsync(id);
            if (entity != null)
            {
                _context.Recruitments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Recruitment>> GetRecruitmentsByStatusAsync(string status)
        {
            return await _context.Recruitments
                .Where(r => r.Status == status)
                .Include(r => r.Employees)
                .ToListAsync();
        }
    }
}
