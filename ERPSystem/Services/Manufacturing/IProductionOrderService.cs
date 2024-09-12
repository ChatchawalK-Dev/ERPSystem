using ERPSystem.Models.Manufacturing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.Manufacturing
{
    public interface IProductionOrderService
    {
        Task<IEnumerable<ProductionOrder>> GetAllAsync();
        Task<ProductionOrder?> GetByIdAsync(int id);
        Task AddAsync(ProductionOrder productionOrder);
        Task UpdateAsync(ProductionOrder productionOrder);
        Task DeleteAsync(int id);
    }

}
