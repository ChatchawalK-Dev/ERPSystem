using ERPSystem.Controllers.Cr;
using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Cr
{
    public class SalesOrderControllerTests
    {
        private readonly Mock<ISalesOrderService> _mockSalesOrderService;
        private readonly SalesOrderController _controller;

        public SalesOrderControllerTests()
        {
            _mockSalesOrderService = new Mock<ISalesOrderService>();
            _controller = new SalesOrderController(_mockSalesOrderService.Object);
        }

        [Fact]
        public async Task GetAllSalesOrders_ReturnsOkResult_WithListOfSalesOrders()
        {
            // Arrange
            var salesOrders = new List<SalesOrder>
            {
                new SalesOrder(),
                new SalesOrder()
            };
            ReflectionHelper.SetProperty(salesOrders[0], "Id", 1);
            ReflectionHelper.SetProperty(salesOrders[1], "Id", 2);

            _mockSalesOrderService.Setup(service => service.GetAllSalesOrdersAsync())
                                  .ReturnsAsync(salesOrders);

            // Act
            var result = await _controller.GetAllSalesOrders();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SalesOrder>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SalesOrder>>(okResult.Value);
            Assert.Equal(2, ((List<SalesOrder>)returnValue).Count);
        }

        [Fact]
        public async Task GetSalesOrderById_ReturnsOkResult_WithSalesOrder()
        {
            // Arrange
            var salesOrderId = 1;
            var salesOrder = new SalesOrder();
            ReflectionHelper.SetProperty(salesOrder, "Id", salesOrderId);

            _mockSalesOrderService.Setup(service => service.GetSalesOrderByIdAsync(salesOrderId))
                                  .ReturnsAsync(salesOrder);

            // Act
            var result = await _controller.GetSalesOrderById(salesOrderId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SalesOrder>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<SalesOrder>(okResult.Value);
            Assert.Equal(salesOrderId, returnValue.Id);
        }

        [Fact]
        public async Task CreateSalesOrder_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var salesOrder = new SalesOrder();
            ReflectionHelper.SetProperty(salesOrder, "Id", 1);

            _mockSalesOrderService.Setup(service => service.CreateSalesOrderAsync(salesOrder))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateSalesOrder(salesOrder);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<SalesOrder>(createdAtActionResult.Value);
            Assert.Equal(salesOrder.Id, returnValue.Id);
            Assert.Equal(nameof(SalesOrderController.GetSalesOrderById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateSalesOrder_ReturnsNoContentResult()
        {
            // Arrange
            var salesOrder = new SalesOrder();
            ReflectionHelper.SetProperty(salesOrder, "Id", 1);

            _mockSalesOrderService.Setup(service => service.UpdateSalesOrderAsync(salesOrder))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateSalesOrder(salesOrder.Id, salesOrder);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSalesOrder_ReturnsNoContentResult()
        {
            // Arrange
            var salesOrderId = 1;

            _mockSalesOrderService.Setup(service => service.DeleteSalesOrderAsync(salesOrderId))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteSalesOrder(salesOrderId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetSalesOrdersByCustomerId_ReturnsOkResult_WithListOfSalesOrders()
        {
            // Arrange
            var customerId = 1;
            var salesOrders = new List<SalesOrder>
            {
                new SalesOrder(),
                new SalesOrder()
            };
            ReflectionHelper.SetProperty(salesOrders[0], "Id", 1);
            ReflectionHelper.SetProperty(salesOrders[1], "Id", 2);

            _mockSalesOrderService.Setup(service => service.GetSalesOrdersByCustomerIdAsync(customerId))
                                  .ReturnsAsync(salesOrders);

            // Act
            var result = await _controller.GetSalesOrdersByCustomerId(customerId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SalesOrder>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SalesOrder>>(okResult.Value);
            Assert.Equal(2, ((List<SalesOrder>)returnValue).Count);
        }
    }
}
