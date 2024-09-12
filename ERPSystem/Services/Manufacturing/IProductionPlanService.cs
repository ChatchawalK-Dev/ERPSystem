using ERPSystem.Models.Manufacturing;

namespace ERPSystem.Services.Manufacturing
{
    public interface IProductionPlanService
    {
        Task<IEnumerable<ProductionPlan>> GetAllAsync();
        Task<ProductionPlan?> GetByIdAsync(int id);
        Task AddAsync(ProductionPlan productionPlan);
        Task UpdateAsync(ProductionPlan productionPlan);
        Task DeleteAsync(int id);
    }
}
