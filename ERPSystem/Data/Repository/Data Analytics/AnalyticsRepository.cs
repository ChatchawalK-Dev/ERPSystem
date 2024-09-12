using ERPSystem.Data;
using ERPSystem.Data.Repository;
using ERPSystem.Models.Data_Analytics;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Repositories
{
    public interface IAnalyticsRepository : IRepository<Analytics>
    {
        // You can add custom methods if needed
    }

    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly ERPDbContext _context;

        public AnalyticsRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<Analytics?> GetByIdAsync(int id)
        {
            return await _context.Analytics
                .Include(a => a.DataReports)  // Include related DataReports if needed
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Analytics>> GetAllAsync()
        {
            return await _context.Analytics
                .Include(a => a.DataReports)  // Include related DataReports if needed
                .ToListAsync();
        }

        public async Task AddAsync(Analytics entity)
        {
            _context.Analytics.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Analytics entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Analytics.FindAsync(id);
            if (entity != null)
            {
                _context.Analytics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
