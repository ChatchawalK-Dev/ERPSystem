using ERPSystem.Models.Finance;
using ERPSystem.Data.Repository.Finance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.Finance
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> GetAllBudgetsAsync();
        Task<Budget?> GetBudgetByIdAsync(int id);
        Task CreateBudgetAsync(Budget budget);
        Task UpdateBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(int id);
        Task<IEnumerable<Budget>> GetBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
        {
            return await _budgetRepository.GetAllAsync();
        }

        public async Task<Budget?> GetBudgetByIdAsync(int id)
        {
            return await _budgetRepository.GetByIdAsync(id);
        }

        public async Task CreateBudgetAsync(Budget budget)
        {
            await _budgetRepository.AddAsync(budget);
        }

        public async Task UpdateBudgetAsync(Budget budget)
        {
            await _budgetRepository.UpdateAsync(budget);
        }

        public async Task DeleteBudgetAsync(int id)
        {
            await _budgetRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _budgetRepository.GetBudgetsByDateRangeAsync(startDate, endDate);
        }
    }
}
