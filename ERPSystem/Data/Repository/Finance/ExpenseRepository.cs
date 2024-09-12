using ERPSystem.Models.Finance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.Finance
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetExpensesByBudgetIdAsync(int budgetId);
    }

    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ERPDbContext _context;

        public ExpenseRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .Include(e => e.Budget)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .Include(e => e.Budget)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Expense entity)
        {
            await _context.Expenses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense entity)
        {
            _context.Expenses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity != null)
            {
                _context.Expenses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetExpensesByBudgetIdAsync(int budgetId)
        {
            return await _context.Expenses
                .Where(e => e.BudgetID == budgetId)
                .Include(e => e.Budget)
                .ToListAsync();
        }
    }
}
