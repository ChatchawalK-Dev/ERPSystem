using ERPSystem.Models.Data_Analytics;
using ERPSystem.Repositories;

namespace ERPSystem.Services.Data_Analytics
{
    public interface IAnalyticsService
    {
        Task<Analytics?> GetByIdAsync(int id);
        Task<IEnumerable<Analytics>> GetAllAsync();
        Task AddAsync(Analytics analytics);
        Task UpdateAsync(Analytics analytics);
        Task DeleteAsync(int id);
    }

    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<Analytics?> GetByIdAsync(int id)
        {
            var result = await _analyticsRepository.GetByIdAsync(id);
            return result; // or handle the result if null
        }


        public async Task<IEnumerable<Analytics>> GetAllAsync()
        {
            return await _analyticsRepository.GetAllAsync();
        }

        public async Task AddAsync(Analytics analytics)
        {
            await _analyticsRepository.AddAsync(analytics);
        }

        public async Task UpdateAsync(Analytics analytics)
        {
            await _analyticsRepository.UpdateAsync(analytics);
        }

        public async Task DeleteAsync(int id)
        {
            await _analyticsRepository.DeleteAsync(id);
        }
    }
}
