using ERPSystem.Models.Supply_Chain;
using ERPSystem.Data.Repository.SupplyChain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.SupplyChain
{
    public interface IDeliveryService
    {
        Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
        Task<Delivery?> GetDeliveryByIdAsync(int id);
        Task CreateDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(int id);
        Task<IEnumerable<Delivery>> GetDeliveriesByPurchaseOrderIdAsync(int purchaseOrderId);
    }

    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            return await _deliveryRepository.GetAllAsync();
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(int id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }

        public async Task CreateDeliveryAsync(Delivery delivery)
        {
            await _deliveryRepository.AddAsync(delivery);
        }

        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            await _deliveryRepository.UpdateAsync(delivery);
        }

        public async Task DeleteDeliveryAsync(int id)
        {
            await _deliveryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByPurchaseOrderIdAsync(int purchaseOrderId)
        {
            return await _deliveryRepository.GetDeliveriesByPurchaseOrderIdAsync(purchaseOrderId);
        }
    }
}
