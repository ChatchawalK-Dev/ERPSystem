using ERPSystem.Data.Repository;
using ERPSystem.Models.Manufacturing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.Manufacturing
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> _inventoryRepository;

        public InventoryService(IRepository<Inventory> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAllItemsAsync()
        {
            return await _inventoryRepository.GetAllAsync();
        }

        public async Task<Inventory?> GetItemByIdAsync(int id)
        {
            return await _inventoryRepository.GetByIdAsync(id);
        }

        public async Task AddItemAsync(Inventory item)
        {
            await _inventoryRepository.AddAsync(item);
        }

        public async Task UpdateItemAsync(Inventory item)
        {
            await _inventoryRepository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _inventoryRepository.DeleteAsync(id);
        }

        public async Task<Inventory?> FindItemAsync(Func<Inventory, bool> predicate)
        {
            var items = await _inventoryRepository.GetAllAsync();
            return items.FirstOrDefault(predicate);
        }
    }
}
