using ERPSystem.Data;
using ERPSystem.Models.Data_Analytics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Repositories
{
    public class DataReportRepository : IDataReportRepository
    {
        private readonly ERPDbContext _context;

        public DataReportRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<DataReport?> GetDataReportByIdAsync(int id)
        {
            return await _context.DataReports
                .Include(dr => dr.Dashboard)
                .Include(dr => dr.Analytics)
                .FirstOrDefaultAsync(dr => dr.Id == id);
        }

        public async Task<IEnumerable<DataReport>> GetAllDataReportsAsync()
        {
            return await _context.DataReports
                .Include(dr => dr.Dashboard)
                .Include(dr => dr.Analytics)
                .ToListAsync();
        }

        public async Task AddDataReportAsync(DataReport dataReport)
        {
            _context.DataReports.Add(dataReport);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDataReportAsync(DataReport dataReport)
        {
            _context.DataReports.Update(dataReport);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDataReportAsync(int id)
        {
            var dataReport = await _context.DataReports.FindAsync(id);
            if (dataReport != null)
            {
                _context.DataReports.Remove(dataReport);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IDataReportRepository
    {
        Task<DataReport?> GetDataReportByIdAsync(int id);
        Task<IEnumerable<DataReport>> GetAllDataReportsAsync();
        Task AddDataReportAsync(DataReport dataReport);
        Task UpdateDataReportAsync(DataReport dataReport);
        Task DeleteDataReportAsync(int id);
    }
}
