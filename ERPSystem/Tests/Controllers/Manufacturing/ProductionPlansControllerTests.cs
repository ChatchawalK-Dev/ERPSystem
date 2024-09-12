using ERPSystem.Controllers.Manufacturing;
using ERPSystem.Models.Finance;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Manufacturing
{
    public class ProductionPlansControllerTests
    {
        private readonly ProductionPlansController _controller;
        private readonly Mock<IProductionPlanService> _mockService;

        public ProductionPlansControllerTests()
        {
            _mockService = new Mock<IProductionPlanService>();
            _controller = new ProductionPlansController(_mockService.Object);
        }

        [Fact]
        public async Task GetProductionPlans_ReturnsOkResult_WithProductionPlans()
        {
            // Arrange
            var productionPlans = new List<ProductionPlan>
        {
            new ProductionPlan { PlanName = "Plan A" },
            new ProductionPlan { PlanName = "Plan B" }
        };
            ReflectionHelper.SetPropertyValue(productionPlans[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(productionPlans[1], "Id", 2);
            _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(productionPlans);

            // Act
            var result = await _controller.GetProductionPlans();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPlans = Assert.IsAssignableFrom<IEnumerable<ProductionPlan>>(okResult.Value);
            Assert.Equal(2, returnedPlans.Count());
        }

        [Fact]
        public async Task GetProductionPlan_ReturnsOkResult_WithProductionPlan()
        {
            // Arrange
            var productionPlan = new ProductionPlan {PlanName = "Plan A" };
            ReflectionHelper.SetPropertyValue(productionPlan, "Id", 1);
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(productionPlan);

            // Act
            var result = await _controller.GetProductionPlan(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPlan = Assert.IsAssignableFrom<ProductionPlan>(okResult.Value);
            Assert.Equal("Plan A", returnedPlan.PlanName);
        }

        [Fact]
        public async Task GetProductionPlan_ReturnsNotFound_WhenPlanDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ProductionPlan?)null);

            // Act
            var result = await _controller.GetProductionPlan(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostProductionPlan_ReturnsCreatedAtActionResult_WhenPlanIsValid()
        {
            // Arrange
            var productionPlan = new ProductionPlan { PlanName = "Plan A" };
            ReflectionHelper.SetPropertyValue(productionPlan, "Id", 1);
            _mockService.Setup(service => service.AddAsync(productionPlan)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostProductionPlan(productionPlan);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetProductionPlan", createdResult.ActionName);

            // Ensure RouteValues is not null
            Assert.NotNull(createdResult.RouteValues);

            // Safely retrieve and check the route value
            Assert.True(createdResult.RouteValues.TryGetValue("id", out var idValue));
            Assert.Equal(1, idValue);
        }

        [Fact]
        public async Task PutProductionPlan_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var productionPlan = new ProductionPlan {PlanName = "Updated Plan" };
            ReflectionHelper.SetPropertyValue(productionPlan, "Id", 1);
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(productionPlan);
            _mockService.Setup(service => service.UpdateAsync(productionPlan)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutProductionPlan(1, productionPlan);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutProductionPlan_ReturnsNotFound_WhenPlanDoesNotExist()
        {
            // Arrange
            var productionPlan = new ProductionPlan { PlanName = "Updated Plan" };
            ReflectionHelper.SetPropertyValue(productionPlan, "Id", 1);

            // Mock the service to return null for the GetByIdAsync method
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((ProductionPlan?)null);

            // Act
            var result = await _controller.PutProductionPlan(1, productionPlan);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task DeleteProductionPlan_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(new ProductionPlan {PlanName = "Plan A" });
            _mockService.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProductionPlan(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProductionPlan_ReturnsNotFound_WhenPlanDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((ProductionPlan?)null);

            // Act
            var result = await _controller.DeleteProductionPlan(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}