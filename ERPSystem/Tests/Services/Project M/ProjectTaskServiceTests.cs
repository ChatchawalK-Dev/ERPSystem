using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using ERPSystem.Data.Repository.ProjectM;

namespace ERPSystem.Tests.Services.ProjectM
{
    public class ProjectTaskServiceTests
    {
        private readonly Mock<IProjectTaskRepository> _mockRepository;
        private readonly ProjectTaskService _service;

        public ProjectTaskServiceTests()
        {
            _mockRepository = new Mock<IProjectTaskRepository>();
            _service = new ProjectTaskService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllTasksAsync_ReturnsAllTasks()
        {
            // Arrange
            var tasks = new List<ProjectTask>
            {
                new ProjectTask { /* Initialize properties */ },
                new ProjectTask { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(tasks[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(tasks[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _service.GetAllTasksAsync();

            // Assert
            Assert.NotNull(result);
            var taskList = result.ToList();
            Assert.Equal(2, taskList.Count);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsCorrectTask()
        {
            // Arrange
            var task = new ProjectTask { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(task, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(task);

            // Act
            var result = await _service.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreateTaskAsync_CallsRepositoryAdd()
        {
            // Arrange
            var task = new ProjectTask { /* Initialize properties */ };

            // Act
            await _service.CreateTaskAsync(task);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(task), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var task = new ProjectTask { /* Initialize properties */ };

            // Act
            await _service.UpdateTaskAsync(task);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteTaskAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetTasksByProjectIdAsync_ReturnsTasksForProject()
        {
            // Arrange
            int projectId = 1;
            var tasks = new List<ProjectTask>
            {
                new ProjectTask { /* Initialize properties */ },
                new ProjectTask { /* Initialize properties */ }
            };
            _mockRepository.Setup(repo => repo.GetTasksByProjectIdAsync(projectId)).ReturnsAsync(tasks);

            // Act
            var result = await _service.GetTasksByProjectIdAsync(projectId);

            // Assert
            Assert.NotNull(result);
            var taskList = result.ToList();
            Assert.Equal(2, taskList.Count);
        }
    }
}
