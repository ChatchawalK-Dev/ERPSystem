using ERPSystem.Controllers.ProjectM;
using ERPSystem.Models.Finance;
using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.ProjectM
{
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectService> _mockService;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            _mockService = new Mock<IProjectService>();
            _controller = new ProjectController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsOkResult_WithProjects()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { ProjectName = "Project A" },
                new Project { ProjectName = "Project B" }
            };
            ReflectionHelper.SetPropertyValue(projects[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(projects[1], "Id", 2);
            _mockService.Setup(service => service.GetAllProjectsAsync()).ReturnsAsync(projects);

            // Act
            var result = await _controller.GetAllProjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProjects = Assert.IsAssignableFrom<IEnumerable<Project>>(okResult.Value);
            Assert.Equal(2, returnedProjects.Count());
        }

        [Fact]
        public async Task GetProjectById_ReturnsOkResult_WithProject()
        {
            // Arrange
            var project = new Project {ProjectName = "Project A" };
            ReflectionHelper.SetPropertyValue(project, "Id", 1);
            _mockService.Setup(service => service.GetProjectByIdAsync(1)).ReturnsAsync(project);

            // Act
            var result = await _controller.GetProjectById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProject = Assert.IsAssignableFrom<Project>(okResult.Value);
            Assert.Equal("Project A", returnedProject.ProjectName);
        }

        [Fact]
        public async Task GetProjectById_ReturnsNotFound_WhenProjectDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetProjectByIdAsync(1)).ReturnsAsync((Project?)null);

            // Act
            var result = await _controller.GetProjectById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateProject_ReturnsCreatedAtActionResult_WhenProjectIsValid()
        {
            // Arrange
            var project = new Project { ProjectName = "New Project" };
            ReflectionHelper.SetPropertyValue(project, "Id", 1);
            _mockService.Setup(service => service.CreateProjectAsync(project)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProject(project);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);
            
            Assert.Equal("GetProjectById", createdResult.ActionName);

            Assert.NotNull(createdResult.RouteValues);
            
            Assert.True(createdResult.RouteValues.TryGetValue("id", out var idValue));
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateProject_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var project = new Project {ProjectName = "Updated Project" };
            ReflectionHelper.SetPropertyValue(project, "Id", 1);
            _mockService.Setup(service => service.UpdateProjectAsync(project)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProject(1, project);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProject_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var project = new Project {ProjectName = "Updated Project" };
            ReflectionHelper.SetPropertyValue(project, "Id", 2);

            // Act
            var result = await _controller.UpdateProject(1, project);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteProjectAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProject(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllProjectsWithTasksAndAllocations_ReturnsOkResult_WithProjects()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project {ProjectName = "Project A"},
                new Project {ProjectName = "Project B"}
            };
            ReflectionHelper.SetPropertyValue(projects[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(projects[1], "Id", 2);
            _mockService.Setup(service => service.GetAllProjectsWithTasksAndAllocationsAsync()).ReturnsAsync(projects);

            // Act
            var result = await _controller.GetAllProjectsWithTasksAndAllocations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProjects = Assert.IsAssignableFrom<IEnumerable<Project>>(okResult.Value);
            Assert.Equal(2, returnedProjects.Count());
        }
    }
}
