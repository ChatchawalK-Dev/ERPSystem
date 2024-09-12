using Moq;
using Xunit;
using ERPSystem.Controllers.Manufacturing;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPSystem.Models.Finance;

namespace ERPSystem.Tests.Controllers.Manufacturing
{
    public class QualityControlControllerTests
    {
        private readonly QualityControlController _controller;
        private readonly Mock<IQualityControlService> _mockService;

        public QualityControlControllerTests()
        {
            _mockService = new Mock<IQualityControlService>();
            _controller = new QualityControlController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllItems_ReturnsOkResult_WithItems()
        {
            // Arrange
            var items = new List<QualityControl>
        {
            new QualityControl { Status = "Passed" },
            new QualityControl { Status = "Failed" }
        };
            ReflectionHelper.SetPropertyValue(items[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(items[1], "Id", 2);
            _mockService.Setup(service => service.GetAllItemsAsync()).ReturnsAsync(items);

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItems = Assert.IsAssignableFrom<IEnumerable<QualityControl>>(okResult.Value);
            Assert.Equal(2, returnItems.Count());
        }

        [Fact]
        public async Task GetItemById_ReturnsOkResult_WithItem()
        {
            // Arrange
            var item = new QualityControl { Status = "Passed" };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);
            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(item);

            // Act
            var result = await _controller.GetItemById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItem = Assert.IsAssignableFrom<QualityControl>(okResult.Value);
            Assert.Equal(1, returnItem.Id);
        }

        [Fact]
        public async Task GetItemById_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync((QualityControl?)null);

            // Act
            var result = await _controller.GetItemById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddItem_ReturnsCreatedAtActionResult_WhenItemIsValid()
        {
            // Arrange
            var item = new QualityControl { Status = "Passed" };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);
            _mockService.Setup(service => service.AddItemAsync(item)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddItem(item);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdResult);

            Assert.NotNull(createdResult.RouteValues);

            Assert.Equal("GetItemById", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateItem_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var item = new QualityControl { Status = "Updated" };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);
            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(item);
            _mockService.Setup(service => service.UpdateItemAsync(item)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateItem(1, item);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateItem_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var item = new QualityControl { Status = "Updated" };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);
            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync((QualityControl?)null);

            // Act
            var result = await _controller.UpdateItem(1, item);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteItem_ReturnsNoContent_WhenItemIsDeleted()
        {
            // Arrange
            var item = new QualityControl { Status = "Passed" };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);
            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(item);
            _mockService.Setup(service => service.DeleteItemAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteItem(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteItem_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync((QualityControl?)null);

            // Act
            var result = await _controller.DeleteItem(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task FindItem_ReturnsOkResult_WhenItemIsFound()
        {
            // Arrange
            var item = new QualityControl { Status = "Passed" };
            ReflectionHelper.SetPropertyValue(item, "Id", 1);
            _mockService.Setup(service => service.FindItemAsync(It.IsAny<Func<QualityControl, bool>>())).ReturnsAsync(item);

            // Act
            var result = await _controller.FindItem("Passed");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItem = Assert.IsAssignableFrom<QualityControl>(okResult.Value);
            Assert.Equal("Passed", returnItem.Status);
        }

        [Fact]
        public async Task FindItem_ReturnsNotFound_WhenItemIsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.FindItemAsync(It.IsAny<Func<QualityControl, bool>>())).ReturnsAsync((QualityControl?)null);

            // Act
            var result = await _controller.FindItem("NotFoundStatus");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
