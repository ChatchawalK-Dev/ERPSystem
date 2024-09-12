using ERPSystem.Data.Repository.Cr;
using ERPSystem.Models.Cr;

namespace ERPSystem.Services.Cr
{
    public interface IMarketingCampaignService
    {
        Task<MarketingCampaign?> GetByIdAsync(int id);
        Task<IEnumerable<MarketingCampaign>> GetAllAsync();
        Task AddAsync(MarketingCampaign marketingCampaign);
        Task UpdateAsync(MarketingCampaign marketingCampaign);
        Task DeleteAsync(int id);
    }
    public class MarketingCampaignService : IMarketingCampaignService
    {
        private readonly IMarketingCampaignRepository _marketingCampaignRepository;

        public MarketingCampaignService(IMarketingCampaignRepository marketingCampaignRepository)
        {
            _marketingCampaignRepository = marketingCampaignRepository;
        }

        public async Task<MarketingCampaign?> GetByIdAsync(int id)
        {
            return await _marketingCampaignRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MarketingCampaign>> GetAllAsync()
        {
            return await _marketingCampaignRepository.GetAllAsync();
        }

        public async Task AddAsync(MarketingCampaign marketingCampaign)
        {
            await _marketingCampaignRepository.AddAsync(marketingCampaign);
        }

        public async Task UpdateAsync(MarketingCampaign marketingCampaign)
        {
            await _marketingCampaignRepository.UpdateAsync(marketingCampaign);
        }

        public async Task DeleteAsync(int id)
        {
            await _marketingCampaignRepository.DeleteAsync(id);
        }
    }
}
