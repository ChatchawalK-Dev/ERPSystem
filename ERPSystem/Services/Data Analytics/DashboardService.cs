using ERPSystem.Models.Data_Analytics;
using ERPSystem.Repositories;

namespace ERPSystem.Services
{
    public interface IDashboardService
    {
        Task<IEnumerable<Dashboard>> GetAllDashboardsAsync();
        Task<Dashboard?> GetDashboardByIdAsync(int id);
        Task AddDashboardAsync(Dashboard dashboard);
        Task UpdateDashboardAsync(Dashboard dashboard);
        Task DeleteDashboardAsync(int id);
    }

    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IEnumerable<Dashboard>> GetAllDashboardsAsync()
        {
            return await _dashboardRepository.GetAllDashboardsAsync();
        }

        public async Task<Dashboard?> GetDashboardByIdAsync(int id)
        {
            return await _dashboardRepository.GetDashboardByIdAsync(id);
        }

        public async Task AddDashboardAsync(Dashboard dashboard)
        {
            await _dashboardRepository.AddDashboardAsync(dashboard);
        }

        public async Task UpdateDashboardAsync(Dashboard dashboard)
        {
            await _dashboardRepository.UpdateDashboardAsync(dashboard);
        }

        public async Task DeleteDashboardAsync(int id)
        {
            await _dashboardRepository.DeleteDashboardAsync(id);
        }
    }
}
