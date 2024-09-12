using ERPSystem.Models.Supply_Chain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.SupplyChain
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersBySupplierIdAsync(int supplierId);
    }

    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ERPDbContext _context;

        public PurchaseOrderRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderDetails)
                .Include(po => po.Deliveries)
                .ToListAsync();
        }

        public async Task<PurchaseOrder?> GetByIdAsync(int id)
        {
            return await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderDetails)
                .Include(po => po.Deliveries)
                .FirstOrDefaultAsync(po => po.Id == id);
        }

        public async Task AddAsync(PurchaseOrder entity)
        {
            await _context.PurchaseOrders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseOrder entity)
        {
            _context.PurchaseOrders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.PurchaseOrders.FindAsync(id);
            if (entity != null)
            {
                _context.PurchaseOrders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersBySupplierIdAsync(int supplierId)
        {
            return await _context.PurchaseOrders
                .Where(po => po.SupplierID == supplierId)
                .Include(po => po.PurchaseOrderDetails)
                .Include(po => po.Deliveries)
                .ToListAsync();
        }
    }
}
