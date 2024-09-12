using ERPSystem.Models.Finance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.Finance
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<IEnumerable<Budget>> GetBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

    public class BudgetRepository : IBudgetRepository
    {
        private readonly ERPDbContext _context;

        public BudgetRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await _context.Budgets
                .Include(b => b.Expenses)
                .ToListAsync();
        }

        public async Task<Budget?> GetByIdAsync(int id)
        {
            return await _context.Budgets
                .Include(b => b.Expenses)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Budget entity)
        {
            await _context.Budgets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Budget entity)
        {
            _context.Budgets.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Budgets.FindAsync(id);
            if (entity != null)
            {
                _context.Budgets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Budget>> GetBudgetsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Budgets
                .Where(b => b.StartDate >= startDate && b.EndDate <= endDate)
                .Include(b => b.Expenses)
                .ToListAsync();
        }
    }
}
