using ERPSystem.Data;
using ERPSystem.Data.Repository;
using ERPSystem.Models.Manufacturing;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Repositories
{
    public class ProductionPlanRepository : IRepository<ProductionPlan>
    {
        private readonly ERPDbContext _context;

        public ProductionPlanRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductionPlan>> GetAllAsync()
        {
            return await _context.ProductionPlans.ToListAsync();
        }

        public async Task<ProductionPlan?> GetByIdAsync(int id)
        {
            return await _context.ProductionPlans.FindAsync(id);
        }

        public async Task AddAsync(ProductionPlan productionPlan)
        {
            _context.ProductionPlans.Add(productionPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductionPlan productionPlan)
        {
            _context.Entry(productionPlan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productionPlan = await _context.ProductionPlans.FindAsync(id);
            if (productionPlan != null)
            {
                _context.ProductionPlans.Remove(productionPlan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
