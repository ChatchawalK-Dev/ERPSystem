using ERPSystem.Models.Cr;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data.Repository.Cr
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        // You can add custom methods if needed
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ERPDbContext _context;

        public CustomerRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.SalesOrders)
                .Include(c => c.MarketingCampaign)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.SalesOrders)
                .Include(c => c.MarketingCampaign)
                .ToListAsync();
        }

        public async Task AddAsync(Customer entity)
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
