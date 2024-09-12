using ERPSystem.Models.Manufacturing;

namespace ERPSystem.Services.Manufacturing
{
    public interface IQualityControlService
    {
        Task<IEnumerable<QualityControl>> GetAllItemsAsync();
        Task<QualityControl?> GetItemByIdAsync(int id);
        Task AddItemAsync(QualityControl item);
        Task UpdateItemAsync(QualityControl item);
        Task DeleteItemAsync(int id);
        Task<QualityControl?> FindItemAsync(Func<QualityControl, bool> predicate);
    }
}
