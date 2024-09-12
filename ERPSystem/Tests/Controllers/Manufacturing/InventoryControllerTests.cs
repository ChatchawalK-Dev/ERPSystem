using ERPSystem.Controllers;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Manufacturing
{
    public class InventoryControllerTests
    {
        private readonly Mock<IInventoryService> _mockInventoryService;
        private readonly Mock<ILogger<InventoryController>> _mockLogger;
        private readonly InventoryController _controller;

        public InventoryControllerTests()
        {
            _mockInventoryService = new Mock<IInventoryService>();
            _mockLogger = new Mock<ILogger<InventoryController>>();
            _controller = new InventoryController(_mockInventoryService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfInventories()
        {
            // Arrange
            var inventories = new List<Inventory>
            {
                new Inventory { ProductID = 101, Quantity = 50 },
                new Inventory { ProductID = 102, Quantity = 100 }
            };
            ReflectionHelper.SetPropertyValue(inventories[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(inventories[1], "Id", 2);
            _mockInventoryService.Setup(service => service.GetAllItemsAsync())
                                .ReturnsAsync(inventories);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedInventories = Assert.IsAssignableFrom<IEnumerable<Inventory>>(okResult.Value);
            Assert.Equal(2, ((List<Inventory>)returnedInventories).Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithInventory()
        {
            // Arrange
            var inventory = new Inventory { ProductID = 101, Quantity = 50 };
            ReflectionHelper.SetPropertyValue(inventory, "Id", 1);
            _mockInventoryService.Setup(service => service.GetItemByIdAsync(1))
                                .ReturnsAsync(inventory);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedInventory = Assert.IsAssignableFrom<Inventory>(okResult.Value);
            Assert.Equal(1, returnedInventory.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var inventory = new Inventory { ProductID = 101, Quantity = 50 };
            ReflectionHelper.SetPropertyValue(inventory, "Id", 1);
            _mockInventoryService.Setup(service => service.AddItemAsync(inventory))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(inventory);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedInventory = Assert.IsAssignableFrom<Inventory>(createdAtActionResult.Value);
            Assert.Equal(inventory.Id, returnedInventory.Id);
            Assert.Equal(nameof(InventoryController.GetById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var inventory = new Inventory { ProductID = 101, Quantity = 50 };
            ReflectionHelper.SetPropertyValue(inventory, "Id", 1);

            // Act
            var result = await _controller.Update(2, inventory);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID mismatch.", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            _mockInventoryService.Setup(service => service.DeleteItemAsync(1))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Find_ReturnsOkResult_WithInventory()
        {
            // Arrange
            var inventory = new Inventory
            {
                ProductID = 101,
                Quantity = 50,
                Product = new Product { ProductName = "Widget" }
            };
            ReflectionHelper.SetPropertyValue(inventory, "Id", 1);
            _mockInventoryService.Setup(service => service.FindItemAsync(It.IsAny<Func<Inventory, bool>>()))
                                .ReturnsAsync(inventory);

            // Act
            var result = await _controller.Find("Widget");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedInventory = Assert.IsAssignableFrom<Inventory>(okResult.Value);

            Assert.NotNull(returnedInventory.Product);
            Assert.Equal("Widget", returnedInventory.Product.ProductName);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult_WhenInventoryNotFound()
        {
            // Arrange
            _mockInventoryService.Setup(service => service.GetItemByIdAsync(1))
                                .ReturnsAsync((Inventory?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Find_ReturnsNotFoundResult_WhenInventoryNotFound()
        {
            // Arrange
            _mockInventoryService.Setup(service => service.FindItemAsync(It.IsAny<Func<Inventory, bool>>()))
                                 .ReturnsAsync((Inventory?)null);

            // Act
            var result = await _controller.Find("NonExistentProduct");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
