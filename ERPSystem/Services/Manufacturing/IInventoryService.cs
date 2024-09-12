using ERPSystem.Models.Manufacturing;

namespace ERPSystem.Services.Manufacturing
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllItemsAsync();
        Task<Inventory?> GetItemByIdAsync(int id);
        Task AddItemAsync(Inventory item);
        Task UpdateItemAsync(Inventory item);
        Task DeleteItemAsync(int id);
        Task<Inventory?> FindItemAsync(Func<Inventory, bool> predicate);
    }
}
