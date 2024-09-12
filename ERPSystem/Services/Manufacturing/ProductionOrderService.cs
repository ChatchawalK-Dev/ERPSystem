using System.Collections.Generic;
using System.Threading.Tasks;
using ERPSystem.Data.Repository;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Repositories;
using ERPSystem.Services;

namespace ERPSystem.Services.Manufacturing
{
    public class ProductionOrderService : IProductionOrderService
    {
        private readonly IRepository<ProductionOrder> _repository;

        public ProductionOrderService(IRepository<ProductionOrder> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductionOrder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductionOrder?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ProductionOrder productionOrder)
        {
            await _repository.AddAsync(productionOrder);
        }

        public async Task UpdateAsync(ProductionOrder productionOrder)
        {
            await _repository.UpdateAsync(productionOrder);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}