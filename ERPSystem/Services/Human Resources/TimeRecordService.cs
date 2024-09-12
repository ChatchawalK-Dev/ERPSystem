using ERPSystem.Models.HumanResources;
using ERPSystem.Data.Repository.HumanResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.HumanResources
{
    public interface ITimeRecordService
    {
        Task<IEnumerable<TimeRecord>> GetAllTimeRecordsAsync();
        Task<TimeRecord?> GetTimeRecordByIdAsync(int id);
        Task CreateTimeRecordAsync(TimeRecord timeRecord);
        Task UpdateTimeRecordAsync(TimeRecord timeRecord);
        Task DeleteTimeRecordAsync(int id);
        Task<IEnumerable<TimeRecord>> GetTimeRecordsByEmployeeIdAsync(int employeeId);
    }

    public class TimeRecordService : ITimeRecordService
    {
        private readonly ITimeRecordRepository _timeRecordRepository;

        public TimeRecordService(ITimeRecordRepository timeRecordRepository)
        {
            _timeRecordRepository = timeRecordRepository;
        }

        public async Task<IEnumerable<TimeRecord>> GetAllTimeRecordsAsync()
        {
            return await _timeRecordRepository.GetAllAsync();
        }

        public async Task<TimeRecord?> GetTimeRecordByIdAsync(int id)
        {
            return await _timeRecordRepository.GetByIdAsync(id);
        }

        public async Task CreateTimeRecordAsync(TimeRecord timeRecord)
        {
            await _timeRecordRepository.AddAsync(timeRecord);
        }

        public async Task UpdateTimeRecordAsync(TimeRecord timeRecord)
        {
            await _timeRecordRepository.UpdateAsync(timeRecord);
        }

        public async Task DeleteTimeRecordAsync(int id)
        {
            await _timeRecordRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TimeRecord>> GetTimeRecordsByEmployeeIdAsync(int employeeId)
        {
            return await _timeRecordRepository.GetTimeRecordsByEmployeeIdAsync(employeeId);
        }
    }
}
