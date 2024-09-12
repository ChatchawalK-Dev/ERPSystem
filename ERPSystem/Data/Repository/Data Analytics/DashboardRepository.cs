using ERPSystem.Data;
using ERPSystem.Models.Data_Analytics;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Repositories
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Dashboard>> GetAllDashboardsAsync();
        Task<Dashboard?> GetDashboardByIdAsync(int id);
        Task AddDashboardAsync(Dashboard dashboard);
        Task UpdateDashboardAsync(Dashboard dashboard);
        Task DeleteDashboardAsync(int id);
    }

    public class DashboardRepository : IDashboardRepository
    {
        private readonly ERPDbContext _context;

        public DashboardRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dashboard>> GetAllDashboardsAsync()
        {
            return await _context.Dashboards.ToListAsync();
        }

        public async Task<Dashboard?> GetDashboardByIdAsync(int id)
        {
            return await _context.Dashboards.FindAsync(id);
        }

        public async Task AddDashboardAsync(Dashboard dashboard)
        {
            _context.Dashboards.Add(dashboard);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDashboardAsync(Dashboard dashboard)
        {
            _context.Entry(dashboard).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDashboardAsync(int id)
        {
            var dashboard = await _context.Dashboards.FindAsync(id);
            if (dashboard != null)
            {
                _context.Dashboards.Remove(dashboard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
