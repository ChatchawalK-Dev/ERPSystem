using ERPSystem.Models.Data_Analytics;
using ERPSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public class DataReportService : IDataReportService
    {
        private readonly IDataReportRepository _dataReportRepository;

        public DataReportService(IDataReportRepository dataReportRepository)
        {
            _dataReportRepository = dataReportRepository;
        }

        public async Task<DataReport?> GetDataReportByIdAsync(int id)
        {
            return await _dataReportRepository.GetDataReportByIdAsync(id);
        }

        public async Task<IEnumerable<DataReport>> GetAllDataReportsAsync()
        {
            return await _dataReportRepository.GetAllDataReportsAsync();
        }

        public async Task AddDataReportAsync(DataReport dataReport)
        {
            await _dataReportRepository.AddDataReportAsync(dataReport);
        }

        public async Task UpdateDataReportAsync(DataReport dataReport)
        {
            await _dataReportRepository.UpdateDataReportAsync(dataReport);
        }

        public async Task DeleteDataReportAsync(int id)
        {
            await _dataReportRepository.DeleteDataReportAsync(id);
        }
    }

    public interface IDataReportService
    {
        Task<DataReport?> GetDataReportByIdAsync(int id);
        Task<IEnumerable<DataReport>> GetAllDataReportsAsync();
        Task AddDataReportAsync(DataReport dataReport);
        Task UpdateDataReportAsync(DataReport dataReport);
        Task DeleteDataReportAsync(int id);
    }
}
