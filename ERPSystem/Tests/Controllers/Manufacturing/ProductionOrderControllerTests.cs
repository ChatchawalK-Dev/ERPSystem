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
    public class ProductionOrderControllerTests
    {
        private readonly ProductionOrderController _controller;
        private readonly Mock<IProductionOrderService> _mockService;

        public ProductionOrderControllerTests()
        {
            _mockService = new Mock<IProductionOrderService>();
            _controller = new ProductionOrderController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfProductionOrders()
        {
            // Arrange
            var orders = new List<ProductionOrder>
            {
                new ProductionOrder { Quantity = 100 ,ProductID = 1,ProductionPlanID = 1},
                new ProductionOrder { Quantity = 100 ,ProductID = 2,ProductionPlanID = 2}
            };
            ReflectionHelper.SetPropertyValue(orders[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(orders[1], "Id", 2);
            _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrders = Assert.IsAssignableFrom<IEnumerable<ProductionOrder>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithProductionOrder()
        {
            // Arrange
            var order = new ProductionOrder { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(order, "Id", 1);
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsAssignableFrom<ProductionOrder>(okResult.Value);
            Assert.Equal(100, returnedOrder.Quantity);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ProductionOrder?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WhenOrderIsValid()
        {
            // Arrange
            var order = new ProductionOrder { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(order, "Id", 1);
            _mockService.Setup(service => service.AddAsync(order)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(order);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.NotNull(createdResult);
            Assert.Equal("GetById", createdResult.ActionName);

            
            var routeValues = createdResult.RouteValues;
            Assert.NotNull(routeValues);
            Assert.True(routeValues.ContainsKey("id"));

          
            Assert.Equal(1, routeValues["id"]);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenOrderIsNull()
        {
            // Act
            var result = await _controller.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var order = new ProductionOrder { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(order, "Id", 2);

            // Act
            var result = await _controller.Update(1, order);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var order = new ProductionOrder { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(order, "Id", 1);
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((ProductionOrder?)null);

            // Act
            var result = await _controller.Update(1, order);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var order = new ProductionOrder { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(order, "Id", 1);
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(order);
            _mockService.Setup(service => service.UpdateAsync(order)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(1, order);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((ProductionOrder?)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var order = new ProductionOrder { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(order, "Id", 1);
            _mockService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(order);
            _mockService.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
