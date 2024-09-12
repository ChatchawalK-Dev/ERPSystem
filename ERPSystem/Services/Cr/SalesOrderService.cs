using ERPSystem.Models.Cr;
using ERPSystem.Data.Repository.Cr;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.Cr
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<SalesOrder>> GetAllSalesOrdersAsync();
        Task<SalesOrder?> GetSalesOrderByIdAsync(int id);
        Task CreateSalesOrderAsync(SalesOrder salesOrder);
        Task UpdateSalesOrderAsync(SalesOrder salesOrder);
        Task DeleteSalesOrderAsync(int id);
        Task<IEnumerable<SalesOrder>> GetSalesOrdersByCustomerIdAsync(int customerId);
    }

    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public SalesOrderService(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllSalesOrdersAsync()
        {
            return await _salesOrderRepository.GetAllAsync();
        }

        public async Task<SalesOrder?> GetSalesOrderByIdAsync(int id)
        {
            return await _salesOrderRepository.GetByIdAsync(id);
        }

        public async Task CreateSalesOrderAsync(SalesOrder salesOrder)
        {
            await _salesOrderRepository.AddAsync(salesOrder);
        }

        public async Task UpdateSalesOrderAsync(SalesOrder salesOrder)
        {
            await _salesOrderRepository.UpdateAsync(salesOrder);
        }

        public async Task DeleteSalesOrderAsync(int id)
        {
            await _salesOrderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SalesOrder>> GetSalesOrdersByCustomerIdAsync(int customerId)
        {
            return await _salesOrderRepository.GetSalesOrdersByCustomerIdAsync(customerId);
        }
    }
}
