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
    public class WarehouseControllerTests
    {
        private readonly WarehouseController _controller;
        private readonly Mock<IWarehouseService> _mockService;

        public WarehouseControllerTests()
        {
            _mockService = new Mock<IWarehouseService>();
            _controller = new WarehouseController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllWarehouses_ReturnsOkResult_WithWarehouses()
        {
            // Arrange
            var warehouses = new List<Warehouse> { new Warehouse { Location = "Warehouse 1" } };
            _mockService.Setup(service => service.GetAllWarehousesAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _controller.GetAllWarehouses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnWarehouses = Assert.IsType<List<Warehouse>>(okResult.Value);
            Assert.Single(returnWarehouses);
        }

        [Fact]
        public async Task GetWarehouseById_ReturnsOkResult_WithWarehouse()
        {
            // Arrange
            var warehouse = new Warehouse { Location = "Warehouse 1" };
            ReflectionHelper.SetPropertyValue(warehouse, "Id", 1);
            _mockService.Setup(service => service.GetWarehouseByIdAsync(1)).ReturnsAsync(warehouse);

            // Act
            var result = await _controller.GetWarehouseById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnWarehouse = Assert.IsType<Warehouse>(okResult.Value);
            Assert.Equal(1, returnWarehouse.Id);
        }

        [Fact]
        public async Task GetWarehouseById_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetWarehouseByIdAsync(1)).ReturnsAsync((Warehouse?)null);

            // Act
            var result = await _controller.GetWarehouseById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateWarehouse_ReturnsCreatedAtActionResult_WhenWarehouseIsValid()
        {
            // Arrange
            var warehouse = new Warehouse { Location = "Warehouse 1" };
            ReflectionHelper.SetPropertyValue(warehouse, "Id", 1);
            _mockService.Setup(service => service.CreateWarehouseAsync(warehouse)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateWarehouse(warehouse);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetWarehouseById", createdResult.ActionName);
            Assert.NotNull(createdResult.RouteValues);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateWarehouse_ReturnsNoContent_WhenWarehouseIsValid()
        {
            // Arrange
            var warehouse = new Warehouse { Location = "Warehouse 1" };
            ReflectionHelper.SetPropertyValue(warehouse, "Id", 1);
            _mockService.Setup(service => service.UpdateWarehouseAsync(warehouse)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateWarehouse(1, warehouse);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateWarehouse_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var warehouse = new Warehouse { Location = "Warehouse 1" };
            ReflectionHelper.SetPropertyValue(warehouse, "Id", 2);

            // Act
            var result = await _controller.UpdateWarehouse(1, warehouse);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteWarehouse_ReturnsNoContent_WhenWarehouseIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteWarehouseAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteWarehouse(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllWarehousesWithInventories_ReturnsOkResult_WithWarehouses()
        {
            // Arrange
            var warehouses = new List<Warehouse> { new Warehouse { Location = "Warehouse 1" } };
            _mockService.Setup(service => service.GetAllWarehousesWithInventoriesAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _controller.GetAllWarehousesWithInventories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnWarehouses = Assert.IsType<List<Warehouse>>(okResult.Value);
            Assert.Single(returnWarehouses);
        }
    }
}
