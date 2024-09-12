using ERPSystem.Models.HumanResources;
using ERPSystem.Data.Repository.HumanResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.HumanResources
{
    public interface ITrainingService
    {
        Task<IEnumerable<Training>> GetAllTrainingsAsync();
        Task<Training?> GetTrainingByIdAsync(int id);
        Task CreateTrainingAsync(Training training);
        Task UpdateTrainingAsync(Training training);
        Task DeleteTrainingAsync(int id);
        Task<IEnumerable<Training>> GetTrainingsByEmployeeIdAsync(int employeeId);
    }

    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<IEnumerable<Training>> GetAllTrainingsAsync()
        {
            return await _trainingRepository.GetAllAsync();
        }

        public async Task<Training?> GetTrainingByIdAsync(int id)
        {
            return await _trainingRepository.GetByIdAsync(id);
        }

        public async Task CreateTrainingAsync(Training training)
        {
            await _trainingRepository.AddAsync(training);
        }

        public async Task UpdateTrainingAsync(Training training)
        {
            await _trainingRepository.UpdateAsync(training);
        }

        public async Task DeleteTrainingAsync(int id)
        {
            await _trainingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Training>> GetTrainingsByEmployeeIdAsync(int employeeId)
        {
            return await _trainingRepository.GetTrainingsByEmployeeIdAsync(employeeId);
        }
    }
}
