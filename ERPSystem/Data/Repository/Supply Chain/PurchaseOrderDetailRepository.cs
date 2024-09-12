using ERPSystem.Models.Supply_Chain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.SupplyChain
{
    public interface IPurchaseOrderDetailRepository : IRepository<PurchaseOrderDetail>
    {
        Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailsByOrderIdAsync(int orderId);
    }

    public class PurchaseOrderDetailRepository : IPurchaseOrderDetailRepository
    {
        private readonly ERPDbContext _context;

        public PurchaseOrderDetailRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetAllAsync()
        {
            return await _context.PurchaseOrderDetails
                .Include(pod => pod.PurchaseOrder)
                .ToListAsync();
        }

        public async Task<PurchaseOrderDetail?> GetByIdAsync(int id)
        {
            return await _context.PurchaseOrderDetails
                .Include(pod => pod.PurchaseOrder)
                .FirstOrDefaultAsync(pod => pod.Id == id);
        }

        public async Task AddAsync(PurchaseOrderDetail entity)
        {
            await _context.PurchaseOrderDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseOrderDetail entity)
        {
            _context.PurchaseOrderDetails.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.PurchaseOrderDetails.FindAsync(id);
            if (entity != null)
            {
                _context.PurchaseOrderDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.PurchaseOrderDetails
                .Where(pod => pod.PurchaseOrderID == orderId)
                .Include(pod => pod.PurchaseOrder)
                .ToListAsync();
        }
    }
}
