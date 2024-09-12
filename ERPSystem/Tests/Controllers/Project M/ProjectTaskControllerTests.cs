using Moq;
using Xunit;
using ERPSystem.Controllers.ProjectM;
using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Tests.Controllers.ProjectM
{
    public class ProjectTaskControllerTests
    {
        private readonly Mock<IProjectTaskService> _mockService;
        private readonly ProjectTaskController _controller;

        public ProjectTaskControllerTests()
        {
            _mockService = new Mock<IProjectTaskService>();
            _controller = new ProjectTaskController(_mockService.Object);
        }

        // Helper method to set private properties if needed
        private void SetPrivateProperty<T>(T obj, string propertyName, object value)
        {
            var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            property?.SetValue(obj, value);
        }

        [Fact]
        public async Task GetAllTasks_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<ProjectTask>
            {
                new ProjectTask { TaskName = "Task 1" }
            };

            // Use reflection to set the Id property for each task
            foreach (var task in tasks)
            {
                ReflectionHelper.SetPropertyValue(task, "Id", 1);
            }

            _mockService.Setup(service => service.GetAllTasksAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetAllTasks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTasks = Assert.IsType<List<ProjectTask>>(okResult.Value);
            Assert.Single(returnTasks);
            Assert.Equal(1, returnTasks[0].Id);
        }


        [Fact]
        public async Task GetTaskById_ReturnsOkResult_WhenTaskExists()
        {
            // Arrange
            var task = new ProjectTask { TaskName = "Task 1" };
            ReflectionHelper.SetPropertyValue(task, "Id", 1);
            _mockService.Setup(service => service.GetTaskByIdAsync(1)).ReturnsAsync(task);

            // Act
            var result = await _controller.GetTaskById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTask = Assert.IsType<ProjectTask>(okResult.Value);
            Assert.Equal(1, returnTask.Id);
        }

        [Fact]
        public async Task CreateTask_ReturnsCreatedAtActionResult_WhenTaskIsValid()
        {
            // Arrange
            var task = new ProjectTask { TaskName = "New Task" };
            ReflectionHelper.SetPropertyValue(task, "Id", 1);
            _mockService.Setup(service => service.CreateTaskAsync(task)).Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.CreateTask(task);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            Assert.NotNull(createdResult);
            Assert.Equal("GetTaskById", createdResult.ActionName);

            Assert.NotNull(createdResult.RouteValues);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateTask_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var task = new ProjectTask { TaskName = "Updated Task" };
            SetPrivateProperty(task, "Id", 2); // Set a different Id to trigger BadRequest

            // Act
            var result = await _controller.UpdateTask(1, task);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateTask_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var task = new ProjectTask { TaskName = "Updated Task" };
            ReflectionHelper.SetPropertyValue(task, "Id", 1);
            _mockService.Setup(service => service.UpdateTaskAsync(task)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateTask(1, task);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTask_ReturnsNoContent_WhenTaskIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteTaskAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTask(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetTasksByProjectId_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<ProjectTask>
            {
                new ProjectTask { TaskName = "Task 1" } 
            };

            foreach (var task in tasks)
            {
                ReflectionHelper.SetPropertyValue(task, "Id", 1);
            }

            _mockService.Setup(service => service.GetTasksByProjectIdAsync(1)).ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetTasksByProjectId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTasks = Assert.IsType<List<ProjectTask>>(okResult.Value);
            Assert.Single(returnTasks);
            Assert.Equal(1, returnTasks[0].Id);
        }
    }
}