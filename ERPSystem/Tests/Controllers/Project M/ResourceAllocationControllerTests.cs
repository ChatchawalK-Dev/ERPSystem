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
    public class ResourceAllocationControllerTests
    {
        private readonly ResourceAllocationController _controller;
        private readonly Mock<IResourceAllocationService> _mockService;

        public ResourceAllocationControllerTests()
        {
            _mockService = new Mock<IResourceAllocationService>();
            _controller = new ResourceAllocationController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllAllocations_ReturnsOkResult_WithAllocations()
        {
            // Arrange
            var allocations = new List<ResourceAllocation> { new ResourceAllocation { Quantity = 100 } };
            _mockService.Setup(service => service.GetAllAllocationsAsync()).ReturnsAsync(allocations);

            // Act
            var result = await _controller.GetAllAllocations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAllocations = Assert.IsType<List<ResourceAllocation>>(okResult.Value);
            Assert.Single(returnAllocations);
        }

        [Fact]
        public async Task GetAllocationById_ReturnsOkResult_WithAllocation()
        {
            // Arrange
            var allocation = new ResourceAllocation { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(allocation, "Id", 1);

            _mockService.Setup(service => service.GetAllocationByIdAsync(1)).ReturnsAsync(allocation);

            // Act
            var result = await _controller.GetAllocationById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAllocation = Assert.IsType<ResourceAllocation>(okResult.Value);
            Assert.Equal(1, returnAllocation.Id);
        }

        [Fact]
        public async Task GetAllocationById_ReturnsNotFound_WhenAllocationDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllocationByIdAsync(1)).ReturnsAsync((ResourceAllocation?)null);

            // Act
            var result = await _controller.GetAllocationById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateAllocation_ReturnsCreatedAtActionResult_WhenAllocationIsValid()
        {
            // Arrange
            var allocation = new ResourceAllocation { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(allocation, "Id", 1);
            _mockService.Setup(service => service.CreateAllocationAsync(allocation)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateAllocation(allocation);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetAllocationById", createdResult.ActionName);

            Assert.NotNull(createdResult.RouteValues);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateAllocation_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var allocation = new ResourceAllocation { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(allocation, "Id", 1);

            // Act
            var result = await _controller.UpdateAllocation(2, allocation);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateAllocation_ReturnsNoContent_WhenAllocationIsUpdated()
        {
            // Arrange
            var allocation = new ResourceAllocation {Quantity = 100 };
            ReflectionHelper.SetPropertyValue(allocation, "Id", 1);
            _mockService.Setup(service => service.UpdateAllocationAsync(allocation)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateAllocation(1, allocation);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAllocation_ReturnsNoContent_WhenAllocationIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteAllocationAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAllocation(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllocationsByProjectId_ReturnsOkResult_WithAllocations()
        {
            // Arrange
            var allocations = new List<ResourceAllocation> { new ResourceAllocation { Quantity = 100 } };
            _mockService.Setup(service => service.GetAllocationsByProjectIdAsync(1)).ReturnsAsync(allocations);

            // Act
            var result = await _controller.GetAllocationsByProjectId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAllocations = Assert.IsType<List<ResourceAllocation>>(okResult.Value);
            Assert.Single(returnAllocations);
        }

        [Fact]
        public async Task GetAllocationsByResourceId_ReturnsOkResult_WithAllocations()
        {
            // Arrange
            var allocations = new List<ResourceAllocation> { new ResourceAllocation { Quantity = 100 } };
            _mockService.Setup(service => service.GetAllocationsByResourceIdAsync(1)).ReturnsAsync(allocations);

            // Act
            var result = await _controller.GetAllocationsByResourceId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAllocations = Assert.IsType<List<ResourceAllocation>>(okResult.Value);
            Assert.Single(returnAllocations);
        }
    }
}
