using ERPSystem.Models.Supply_Chain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.SupplyChain
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<IEnumerable<Warehouse>> GetAllWarehousesWithInventoriesAsync();
    }

    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ERPDbContext _context;

        public WarehouseRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses
                .Include(w => w.Inventories)
                .ToListAsync();
        }

        public async Task<Warehouse?> GetByIdAsync(int id)
        {
            return await _context.Warehouses
                .Include(w => w.Inventories)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddAsync(Warehouse entity)
        {
            await _context.Warehouses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Warehouse entity)
        {
            _context.Warehouses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Warehouses.FindAsync(id);
            if (entity != null)
            {
                _context.Warehouses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesWithInventoriesAsync()
        {
            return await _context.Warehouses
                .Include(w => w.Inventories)
                .ToListAsync();
        }
    }
}
