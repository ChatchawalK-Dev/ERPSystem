using ERPSystem.Models.Manufacturing;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data.Repository.Manufacturing
{
    public class InventoryRepository : IRepository<Inventory>
    {
        private readonly ERPDbContext _context;
        private readonly DbSet<Inventory> _dbSet;

        public InventoryRepository(ERPDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Inventory>();
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _dbSet.Include(i => i.Product).ToListAsync();
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.Product).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Inventory entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Inventory entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
