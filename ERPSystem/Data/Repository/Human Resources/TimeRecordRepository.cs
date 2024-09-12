using ERPSystem.Models.HumanResources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.HumanResources
{
    public interface ITimeRecordRepository : IRepository<TimeRecord>
    {
        Task<IEnumerable<TimeRecord>> GetTimeRecordsByEmployeeIdAsync(int employeeId);
    }

    public class TimeRecordRepository : ITimeRecordRepository
    {
        private readonly ERPDbContext _context;

        public TimeRecordRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeRecord>> GetAllAsync()
        {
            return await _context.TimeRecords
                .Include(tr => tr.Employee)
                .ToListAsync();
        }

        public async Task<TimeRecord?> GetByIdAsync(int id)
        {
            return await _context.TimeRecords
                .Include(tr => tr.Employee)
                .FirstOrDefaultAsync(tr => tr.Id == id);
        }

        public async Task AddAsync(TimeRecord entity)
        {
            await _context.TimeRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeRecord entity)
        {
            _context.TimeRecords.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TimeRecords.FindAsync(id);
            if (entity != null)
            {
                _context.TimeRecords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TimeRecord>> GetTimeRecordsByEmployeeIdAsync(int employeeId)
        {
            return await _context.TimeRecords
                .Where(tr => tr.EmployeeID == employeeId)
                .Include(tr => tr.Employee)
                .ToListAsync();
        }
    }
}
