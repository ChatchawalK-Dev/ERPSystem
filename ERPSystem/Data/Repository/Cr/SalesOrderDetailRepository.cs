using ERPSystem.Data.Repository;
using ERPSystem.Models.Cr;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data.Repository.Cr
{
    public interface ISalesOrderDetailRepository : IRepository<SalesOrderDetail>
    {
        Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetailsByOrderIdAsync(int orderId);
    }

    public class SalesOrderDetailRepository : ISalesOrderDetailRepository
    {
        private readonly ERPDbContext _context;
        public SalesOrderDetailRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.SalesOrderDetails
                .Where(s => s.SalesOrderID == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SalesOrderDetail>> GetAllAsync()
        {
            return await _context.SalesOrderDetails.ToListAsync();
        }

        public async Task<SalesOrderDetail?> GetByIdAsync(int id)
        {
            return await _context.SalesOrderDetails.FindAsync(id);
        }

        public async Task AddAsync(SalesOrderDetail entity)
        {
            await _context.SalesOrderDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesOrderDetail entity)
        {
            _context.SalesOrderDetails.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SalesOrderDetails.FindAsync(id);
            if (entity != null)
            {
                _context.SalesOrderDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
