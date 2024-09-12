using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ERPSystem.Controllers.SupplyChain;
using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Tests.Controllers.Supply_Chain
{
    public class DeliveryControllerTests
    {
        private readonly DeliveryController _controller;
        private readonly Mock<IDeliveryService> _mockService;

        public DeliveryControllerTests()
        {
            _mockService = new Mock<IDeliveryService>();
            _controller = new DeliveryController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllDeliveries_ReturnsOkResult_WithDeliveries()
        {
            // Arrange
            var deliveries = new List<Delivery> { new Delivery { DeliveryDate = DateTime.UtcNow } };
            _mockService.Setup(service => service.GetAllDeliveriesAsync()).ReturnsAsync(deliveries);

            // Act
            var result = await _controller.GetAllDeliveries();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnDeliveries = Assert.IsType<List<Delivery>>(okResult.Value);
            Assert.Single(returnDeliveries);
        }

        [Fact]
        public async Task GetDeliveryById_ReturnsOkResult_WithDelivery()
        {
            // Arrange
            var delivery = new Delivery { DeliveryDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(delivery, "Id", 1);
            _mockService.Setup(service => service.GetDeliveryByIdAsync(1)).ReturnsAsync(delivery);

            // Act
            var result = await _controller.GetDeliveryById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnDelivery = Assert.IsType<Delivery>(okResult.Value);
            Assert.Equal(1, returnDelivery.Id);
        }

        [Fact]
        public async Task GetDeliveryById_ReturnsNotFound_WhenDeliveryDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetDeliveryByIdAsync(1)).ReturnsAsync((Delivery?)null);

            // Act
            var result = await _controller.GetDeliveryById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateDelivery_ReturnsCreatedAtActionResult_WhenDeliveryIsValid()
        {
            // Arrange
            var delivery = new Delivery { DeliveryDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(delivery, "Id", 1);
            _mockService.Setup(service => service.CreateDeliveryAsync(delivery)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateDelivery(delivery);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetDeliveryById", createdResult.ActionName);
            Assert.NotNull(createdResult.RouteValues);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateDelivery_ReturnsNoContent_WhenDeliveryIsValid()
        {
            // Arrange
            var delivery = new Delivery { DeliveryDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(delivery, "Id", 1);
            _mockService.Setup(service => service.UpdateDeliveryAsync(delivery)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateDelivery(1, delivery);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateDelivery_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var delivery = new Delivery { DeliveryDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(delivery, "Id", 2);

            // Act
            var result = await _controller.UpdateDelivery(1, delivery);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteDelivery_ReturnsNoContent_WhenDeliveryIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteDeliveryAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteDelivery(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetDeliveriesByPurchaseOrderId_ReturnsOkResult_WithDeliveries()
        {
            // Arrange
            var deliveries = new List<Delivery> { new Delivery { DeliveryDate = DateTime.UtcNow } };
            _mockService.Setup(service => service.GetDeliveriesByPurchaseOrderIdAsync(1)).ReturnsAsync(deliveries);

            // Act
            var result = await _controller.GetDeliveriesByPurchaseOrderId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnDeliveries = Assert.IsType<List<Delivery>>(okResult.Value);
            Assert.Single(returnDeliveries);
        }
    }
}
