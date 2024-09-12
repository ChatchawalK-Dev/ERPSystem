using ERPSystem.Data;
using ERPSystem.Models.Manufacturing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.Manufacturing
{
    public class ProductionOrderRepository : IRepository<ProductionOrder>
    {
        private readonly ERPDbContext _context;
        private readonly DbSet<ProductionOrder> _dbSet;

        public ProductionOrderRepository(ERPDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ProductionOrder>();
        }

        public async Task<IEnumerable<ProductionOrder>> GetAllAsync()
        {
            return await _dbSet.Include(po => po.Product)
                               .Include(po => po.ProductionPlan)
                               .ToListAsync();
        }

        public async Task<ProductionOrder?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(po => po.Product)
                               .Include(po => po.ProductionPlan)
                               .FirstOrDefaultAsync(po => po.Id == id);
        }

        public async Task AddAsync(ProductionOrder entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductionOrder entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
