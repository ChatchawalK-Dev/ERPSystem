using ERPSystem.Models.Supply_Chain;
using ERPSystem.Data.Repository.SupplyChain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.SupplyChain
{
    public interface IPurchaseOrderDetailService
    {
        Task<IEnumerable<PurchaseOrderDetail>> GetAllPurchaseOrderDetailsAsync();
        Task<PurchaseOrderDetail?> GetPurchaseOrderDetailByIdAsync(int id);
        Task CreatePurchaseOrderDetailAsync(PurchaseOrderDetail purchaseOrderDetail);
        Task UpdatePurchaseOrderDetailAsync(PurchaseOrderDetail purchaseOrderDetail);
        Task DeletePurchaseOrderDetailAsync(int id);
        Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailsByOrderIdAsync(int orderId);
    }

    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

        public PurchaseOrderDetailService(IPurchaseOrderDetailRepository purchaseOrderDetailRepository)
        {
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetAllPurchaseOrderDetailsAsync()
        {
            return await _purchaseOrderDetailRepository.GetAllAsync();
        }

        public async Task<PurchaseOrderDetail?> GetPurchaseOrderDetailByIdAsync(int id)
        {
            return await _purchaseOrderDetailRepository.GetByIdAsync(id);
        }

        public async Task CreatePurchaseOrderDetailAsync(PurchaseOrderDetail purchaseOrderDetail)
        {
            await _purchaseOrderDetailRepository.AddAsync(purchaseOrderDetail);
        }

        public async Task UpdatePurchaseOrderDetailAsync(PurchaseOrderDetail purchaseOrderDetail)
        {
            await _purchaseOrderDetailRepository.UpdateAsync(purchaseOrderDetail);
        }

        public async Task DeletePurchaseOrderDetailAsync(int id)
        {
            await _purchaseOrderDetailRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _purchaseOrderDetailRepository.GetPurchaseOrderDetailsByOrderIdAsync(orderId);
        }
    }
}
