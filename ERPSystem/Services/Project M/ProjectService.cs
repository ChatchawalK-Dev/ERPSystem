using ERPSystem.Models.Project_M;
using ERPSystem.Data.Repository.ProjectM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.ProjectM
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int id);
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsWithTasksAndAllocationsAsync();
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteProjectAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsWithTasksAndAllocationsAsync()
        {
            return await _projectRepository.GetAllProjectsWithTasksAndAllocationsAsync();
        }
    }
}
