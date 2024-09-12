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
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _mockRepository;
        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            _mockRepository = new Mock<IProjectRepository>();
            _service = new ProjectService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllProjectsAsync_ReturnsAllProjects()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { /* Initialize properties */ },
                new Project { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(projects[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(projects[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(projects);

            // Act
            var result = await _service.GetAllProjectsAsync();

            // Assert
            Assert.NotNull(result);
            var projectList = result.ToList();
            Assert.Equal(2, projectList.Count);
        }

        [Fact]
        public async Task GetProjectByIdAsync_ReturnsCorrectProject()
        {
            // Arrange
            var project = new Project { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(project, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(project);

            // Act
            var result = await _service.GetProjectByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreateProjectAsync_CallsRepositoryAdd()
        {
            // Arrange
            var project = new Project { /* Initialize properties */ };

            // Act
            await _service.CreateProjectAsync(project);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(project), Times.Once);
        }

        [Fact]
        public async Task UpdateProjectAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var project = new Project { /* Initialize properties */ };

            // Act
            await _service.UpdateProjectAsync(project);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(project), Times.Once);
        }

        [Fact]
        public async Task DeleteProjectAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteProjectAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllProjectsWithTasksAndAllocationsAsync_ReturnsProjectsWithTasksAndAllocations()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { /* Initialize properties */ },
                new Project { /* Initialize properties */ }
            };
            _mockRepository.Setup(repo => repo.GetAllProjectsWithTasksAndAllocationsAsync()).ReturnsAsync(projects);

            // Act
            var result = await _service.GetAllProjectsWithTasksAndAllocationsAsync();

            // Assert
            Assert.NotNull(result);
            var projectList = result.ToList();
            Assert.Equal(2, projectList.Count);
        }
    }
}
