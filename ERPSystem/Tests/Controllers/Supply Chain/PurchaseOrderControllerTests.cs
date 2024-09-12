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
    public class PurchaseOrderControllerTests
    {
        private readonly PurchaseOrderController _controller;
        private readonly Mock<IPurchaseOrderService> _mockService;

        public PurchaseOrderControllerTests()
        {
            _mockService = new Mock<IPurchaseOrderService>();
            _controller = new PurchaseOrderController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllPurchaseOrders_ReturnsOkResult_WithPurchaseOrders()
        {
            // Arrange
            var purchaseOrders = new List<PurchaseOrder> { new PurchaseOrder { OrderDate = DateTime.UtcNow } };
            _mockService.Setup(service => service.GetAllPurchaseOrdersAsync()).ReturnsAsync(purchaseOrders);

            // Act
            var result = await _controller.GetAllPurchaseOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPurchaseOrders = Assert.IsType<List<PurchaseOrder>>(okResult.Value);
            Assert.Single(returnPurchaseOrders);
        }

        [Fact]
        public async Task GetPurchaseOrderById_ReturnsOkResult_WithPurchaseOrder()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { OrderDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(purchaseOrder, "Id", 1);
            _mockService.Setup(service => service.GetPurchaseOrderByIdAsync(1)).ReturnsAsync(purchaseOrder);

            // Act
            var result = await _controller.GetPurchaseOrderById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPurchaseOrder = Assert.IsType<PurchaseOrder>(okResult.Value);
            Assert.Equal(1, returnPurchaseOrder.Id);
        }

        [Fact]
        public async Task GetPurchaseOrderById_ReturnsNotFound_WhenPurchaseOrderDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetPurchaseOrderByIdAsync(1)).ReturnsAsync((PurchaseOrder?)null);

            // Act
            var result = await _controller.GetPurchaseOrderById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePurchaseOrder_ReturnsCreatedAtActionResult_WhenPurchaseOrderIsValid()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { OrderDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(purchaseOrder, "Id", 1);
            _mockService.Setup(service => service.CreatePurchaseOrderAsync(purchaseOrder)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreatePurchaseOrder(purchaseOrder);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetPurchaseOrderById", createdResult.ActionName);
            Assert.NotNull(createdResult.RouteValues);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdatePurchaseOrder_ReturnsNoContent_WhenPurchaseOrderIsValid()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { OrderDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(purchaseOrder, "Id", 1);
            _mockService.Setup(service => service.UpdatePurchaseOrderAsync(purchaseOrder)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePurchaseOrder(1, purchaseOrder);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePurchaseOrder_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { OrderDate = DateTime.UtcNow };
            ReflectionHelper.SetPropertyValue(purchaseOrder, "Id", 2);

            // Act
            var result = await _controller.UpdatePurchaseOrder(1, purchaseOrder);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePurchaseOrder_ReturnsNoContent_WhenPurchaseOrderIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeletePurchaseOrderAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePurchaseOrder(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetPurchaseOrdersBySupplierId_ReturnsOkResult_WithPurchaseOrders()
        {
            // Arrange
            var purchaseOrders = new List<PurchaseOrder> { new PurchaseOrder { OrderDate = DateTime.UtcNow } };
            _mockService.Setup(service => service.GetPurchaseOrdersBySupplierIdAsync(1)).ReturnsAsync(purchaseOrders);

            // Act
            var result = await _controller.GetPurchaseOrdersBySupplierId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPurchaseOrders = Assert.IsType<List<PurchaseOrder>>(okResult.Value);
            Assert.Single(returnPurchaseOrders);
        }
    }
}
