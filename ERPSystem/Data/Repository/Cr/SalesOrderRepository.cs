using ERPSystem.Models.Cr;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.Cr
{
    public interface ISalesOrderRepository : IRepository<SalesOrder>
    {
        Task<IEnumerable<SalesOrder>> GetSalesOrdersByCustomerIdAsync(int customerId);
    }

    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly ERPDbContext _context;

        public SalesOrderRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllAsync()
        {
            return await _context.SalesOrders
                .Include(so => so.SalesOrderDetails)
                .ToListAsync();
        }

        public async Task<SalesOrder?> GetByIdAsync(int id)
        {
            return await _context.SalesOrders
                .Include(so => so.SalesOrderDetails)
                .FirstOrDefaultAsync(so => so.Id == id);
        }

        public async Task AddAsync(SalesOrder entity)
        {
            await _context.SalesOrders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesOrder entity)
        {
            _context.SalesOrders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SalesOrders.FindAsync(id);
            if (entity != null)
            {
                _context.SalesOrders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SalesOrder>> GetSalesOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.SalesOrders
                .Include(so => so.SalesOrderDetails)
                .Where(so => so.CustomerID == customerId)
                .ToListAsync();
        }
    }
}
