using ERPSystem.Models.Supply_Chain;
using ERPSystem.Data.Repository.SupplyChain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.SupplyChain
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync();
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id);
        Task CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task DeletePurchaseOrderAsync(int id);
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersBySupplierIdAsync(int supplierId);
    }

    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            return await _purchaseOrderRepository.GetAllAsync();
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id)
        {
            return await _purchaseOrderRepository.GetByIdAsync(id);
        }

        public async Task CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderRepository.AddAsync(purchaseOrder);
        }

        public async Task UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
        }

        public async Task DeletePurchaseOrderAsync(int id)
        {
            await _purchaseOrderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersBySupplierIdAsync(int supplierId)
        {
            return await _purchaseOrderRepository.GetPurchaseOrdersBySupplierIdAsync(supplierId);
        }
    }
}
