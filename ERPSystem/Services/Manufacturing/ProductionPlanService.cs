using ERPSystem.Models.Manufacturing;
using ERPSystem.Data.Repository;

namespace ERPSystem.Services.Manufacturing
{
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly IRepository<ProductionPlan> _repository;

        public ProductionPlanService(IRepository<ProductionPlan> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductionPlan>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductionPlan?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ProductionPlan productionPlan)
        {
            await _repository.AddAsync(productionPlan);
        }

        public async Task UpdateAsync(ProductionPlan productionPlan)
        {
            await _repository.UpdateAsync(productionPlan);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
