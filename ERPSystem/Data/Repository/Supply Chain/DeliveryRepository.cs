using ERPSystem.Models.Supply_Chain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.SupplyChain
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        Task<IEnumerable<Delivery>> GetDeliveriesByPurchaseOrderIdAsync(int purchaseOrderId);
    }

    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ERPDbContext _context;

        public DeliveryRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _context.Deliveries
                .Include(d => d.PurchaseOrder)
                .ToListAsync();
        }

        public async Task<Delivery?> GetByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.PurchaseOrder)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Delivery entity)
        {
            await _context.Deliveries.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Delivery entity)
        {
            _context.Deliveries.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Deliveries.FindAsync(id);
            if (entity != null)
            {
                _context.Deliveries.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByPurchaseOrderIdAsync(int purchaseOrderId)
        {
            return await _context.Deliveries
                .Where(d => d.PurchaseOrderID == purchaseOrderId)
                .Include(d => d.PurchaseOrder)
                .ToListAsync();
        }
    }
}
