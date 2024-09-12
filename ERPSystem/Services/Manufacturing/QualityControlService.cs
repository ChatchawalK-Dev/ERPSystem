using ERPSystem.Data.Repository;
using ERPSystem.Models.Manufacturing;

namespace ERPSystem.Services.Manufacturing
{
    public class QualityControlService : IQualityControlService
    {
        private readonly IRepository<QualityControl> _repository;

        public QualityControlService(IRepository<QualityControl> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<QualityControl>> GetAllItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<QualityControl?> GetItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddItemAsync(QualityControl item)
        {
            await _repository.AddAsync(item);
        }

        public async Task UpdateItemAsync(QualityControl item)
        {
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<QualityControl?> FindItemAsync(Func<QualityControl, bool> predicate)
        {
            var items = await _repository.GetAllAsync();
            return items.FirstOrDefault(predicate);
        }
    }
}
