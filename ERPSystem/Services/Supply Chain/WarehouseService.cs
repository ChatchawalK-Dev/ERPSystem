using ERPSystem.Models.Supply_Chain;
using ERPSystem.Data.Repository.SupplyChain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.SupplyChain
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse?> GetWarehouseByIdAsync(int id);
        Task CreateWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse);
        Task DeleteWarehouseAsync(int id);
        Task<IEnumerable<Warehouse>> GetAllWarehousesWithInventoriesAsync();
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
        {
            return await _warehouseRepository.GetAllAsync();
        }

        public async Task<Warehouse?> GetWarehouseByIdAsync(int id)
        {
            return await _warehouseRepository.GetByIdAsync(id);
        }

        public async Task CreateWarehouseAsync(Warehouse warehouse)
        {
            await _warehouseRepository.AddAsync(warehouse);
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            await _warehouseRepository.UpdateAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            await _warehouseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesWithInventoriesAsync()
        {
            return await _warehouseRepository.GetAllWarehousesWithInventoriesAsync();
        }
    }
}
