using ERPSystem.Data;
using ERPSystem.Models.Cr;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data.Repository.Cr
{
    public interface IMarketingCampaignRepository : IRepository<MarketingCampaign>
    {
       
    }
    public class MarketingCampaignRepository : IMarketingCampaignRepository
    {
        private readonly ERPDbContext _context;

        public MarketingCampaignRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<MarketingCampaign?> GetByIdAsync(int id)
        {
            return await _context.MarketingCampaigns
                .Include(mc => mc.Customers) 
                .FirstOrDefaultAsync(mc => mc.Id == id);
        }

        public async Task<IEnumerable<MarketingCampaign>> GetAllAsync()
        {
            return await _context.MarketingCampaigns
                .Include(mc => mc.Customers)
                .ToListAsync();
        }

        public async Task AddAsync(MarketingCampaign entity)
        {
            _context.MarketingCampaigns.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MarketingCampaign entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MarketingCampaigns.FindAsync(id);
            if (entity != null)
            {
                _context.MarketingCampaigns.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
