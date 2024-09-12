using ERPSystem.Models.Project_M;
using ERPSystem.Data.Repository.ProjectM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.ProjectM
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask?> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(ProjectTask task);
        Task UpdateTaskAsync(ProjectTask task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
    }

    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            return await _projectTaskRepository.GetAllAsync();
        }

        public async Task<ProjectTask?> GetTaskByIdAsync(int id)
        {
            return await _projectTaskRepository.GetByIdAsync(id);
        }

        public async Task CreateTaskAsync(ProjectTask task)
        {
            await _projectTaskRepository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(ProjectTask task)
        {
            await _projectTaskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _projectTaskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _projectTaskRepository.GetTasksByProjectIdAsync(projectId);
        }
    }
}
