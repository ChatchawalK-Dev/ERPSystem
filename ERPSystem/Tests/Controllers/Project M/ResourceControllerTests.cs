using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ERPSystem.Controllers.ProjectM;
using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Tests.Controllers.ProjectM
{
    public class ResourceControllerTests
    {
        private readonly ResourceController _controller;
        private readonly Mock<IResourceService> _mockService;

        public ResourceControllerTests()
        {
            _mockService = new Mock<IResourceService>();
            _controller = new ResourceController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllResources_ReturnsOkResult_WithResources()
        {
            // Arrange
            var resources = new List<Resource> { new Resource { ResourceName = "Resource 1" } };
            _mockService.Setup(service => service.GetAllResourcesAsync()).ReturnsAsync(resources);

            // Act
            var result = await _controller.GetAllResources();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnResources = Assert.IsType<List<Resource>>(okResult.Value);
            Assert.Single(returnResources);
        }

        [Fact]
        public async Task GetResourceById_ReturnsOkResult_WithResource()
        {
            // Arrange
            var resource = new Resource { ResourceName = "Resource 1" };
            ReflectionHelper.SetPropertyValue(resource, "Id", 1);
            _mockService.Setup(service => service.GetResourceByIdAsync(1)).ReturnsAsync(resource);

            // Act
            var result = await _controller.GetResourceById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnResource = Assert.IsType<Resource>(okResult.Value);
            Assert.Equal("Resource 1", returnResource.ResourceName);
        }

        [Fact]
        public async Task GetResourceById_ReturnsNotFound_WhenResourceDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetResourceByIdAsync(1)).ReturnsAsync((Resource?)null);

            // Act
            var result = await _controller.GetResourceById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateResource_ReturnsCreatedAtActionResult_WhenResourceIsValid()
        {
            // Arrange
            var resource = new Resource { ResourceName = "New Resource" };
            ReflectionHelper.SetPropertyValue(resource, "Id", 1);
            _mockService.Setup(service => service.CreateResourceAsync(resource)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateResource(resource);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetResourceById", createdResult.ActionName);

            Assert.NotNull(createdResult.RouteValues);

            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateResource_ReturnsNoContent_WhenResourceIsValid()
        {
            // Arrange
            var resource = new Resource { ResourceName = "Updated Resource" };
            ReflectionHelper.SetPropertyValue(resource, "Id", 1);
            _mockService.Setup(service => service.UpdateResourceAsync(resource)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateResource(1, resource);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateResource_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var resource = new Resource { ResourceName = "Updated Resource" };
            ReflectionHelper.SetPropertyValue(resource, "Id", 2);

            // Act
            var result = await _controller.UpdateResource(1, resource);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteResource_ReturnsNoContent_WhenResourceIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteResourceAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteResource(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
