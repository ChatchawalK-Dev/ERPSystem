using ERPSystem.Models.Cr;
using ERPSystem.Data.Repository.Cr;

namespace ERPSystem.Services.Cr
{ 
    public interface ISalesOrderDetailService
        {
            Task<IEnumerable<SalesOrderDetail>> GetAllSalesOrderDetailsAsync();
            Task<SalesOrderDetail?> GetSalesOrderDetailByIdAsync(int id);
            Task CreateSalesOrderDetailAsync(SalesOrderDetail salesOrderDetail);
            Task UpdateSalesOrderDetailAsync(SalesOrderDetail salesOrderDetail);
            Task DeleteSalesOrderDetailAsync(int id);
        }
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;

        public SalesOrderDetailService(ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public async Task<IEnumerable<SalesOrderDetail>> GetAllSalesOrderDetailsAsync()
        {
            return await _salesOrderDetailRepository.GetAllAsync();
        }

        public async Task<SalesOrderDetail?> GetSalesOrderDetailByIdAsync(int id)
        {
            return await _salesOrderDetailRepository.GetByIdAsync(id);
        }

        public async Task CreateSalesOrderDetailAsync(SalesOrderDetail salesOrderDetail)
        {
            await _salesOrderDetailRepository.AddAsync(salesOrderDetail);
        }

        public async Task UpdateSalesOrderDetailAsync(SalesOrderDetail salesOrderDetail)
        {
            await _salesOrderDetailRepository.UpdateAsync(salesOrderDetail);
        }

        public async Task DeleteSalesOrderDetailAsync(int id)
        {
            await _salesOrderDetailRepository.DeleteAsync(id);
        }
    }
}
