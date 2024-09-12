using ERPSystem.Models.HumanResources;
using ERPSystem.Data.Repository.HumanResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.HumanResources
{
    public interface IRecruitmentService
    {
        Task<IEnumerable<Recruitment>> GetAllRecruitmentsAsync();
        Task<Recruitment?> GetRecruitmentByIdAsync(int id);
        Task CreateRecruitmentAsync(Recruitment recruitment);
        Task UpdateRecruitmentAsync(Recruitment recruitment);
        Task DeleteRecruitmentAsync(int id);
        Task<IEnumerable<Recruitment>> GetRecruitmentsByStatusAsync(string status);
    }

    public class RecruitmentService : IRecruitmentService
    {
        private readonly IRecruitmentRepository _recruitmentRepository;

        public RecruitmentService(IRecruitmentRepository recruitmentRepository)
        {
            _recruitmentRepository = recruitmentRepository;
        }

        public async Task<IEnumerable<Recruitment>> GetAllRecruitmentsAsync()
        {
            return await _recruitmentRepository.GetAllAsync();
        }

        public async Task<Recruitment?> GetRecruitmentByIdAsync(int id)
        {
            return await _recruitmentRepository.GetByIdAsync(id);
        }

        public async Task CreateRecruitmentAsync(Recruitment recruitment)
        {
            await _recruitmentRepository.AddAsync(recruitment);
        }

        public async Task UpdateRecruitmentAsync(Recruitment recruitment)
        {
            await _recruitmentRepository.UpdateAsync(recruitment);
        }

        public async Task DeleteRecruitmentAsync(int id)
        {
            await _recruitmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recruitment>> GetRecruitmentsByStatusAsync(string status)
        {
            return await _recruitmentRepository.GetRecruitmentsByStatusAsync(status);
        }
    }
}
