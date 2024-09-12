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
    public class PurchaseOrderDetailControllerTests
    {
        private readonly PurchaseOrderDetailController _controller;
        private readonly Mock<IPurchaseOrderDetailService> _mockService;

        public PurchaseOrderDetailControllerTests()
        {
            _mockService = new Mock<IPurchaseOrderDetailService>();
            _controller = new PurchaseOrderDetailController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllPurchaseOrderDetails_ReturnsOkResult_WithPurchaseOrderDetails()
        {
            // Arrange
            var purchaseOrderDetails = new List<PurchaseOrderDetail> { new PurchaseOrderDetail { Quantity = 10 } };
            _mockService.Setup(service => service.GetAllPurchaseOrderDetailsAsync()).ReturnsAsync(purchaseOrderDetails);

            // Act
            var result = await _controller.GetAllPurchaseOrderDetails();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPurchaseOrderDetails = Assert.IsType<List<PurchaseOrderDetail>>(okResult.Value);
            Assert.Single(returnPurchaseOrderDetails);
        }

        [Fact]
        public async Task GetPurchaseOrderDetailById_ReturnsOkResult_WithPurchaseOrderDetail()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { Quantity = 10 };
            ReflectionHelper.SetPropertyValue(purchaseOrderDetail, "Id", 1);
            _mockService.Setup(service => service.GetPurchaseOrderDetailByIdAsync(1)).ReturnsAsync(purchaseOrderDetail);

            // Act
            var result = await _controller.GetPurchaseOrderDetailById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPurchaseOrderDetail = Assert.IsType<PurchaseOrderDetail>(okResult.Value);
            Assert.Equal(1, returnPurchaseOrderDetail.Id);
        }

        [Fact]
        public async Task GetPurchaseOrderDetailById_ReturnsNotFound_WhenPurchaseOrderDetailDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetPurchaseOrderDetailByIdAsync(1)).ReturnsAsync((PurchaseOrderDetail?)null);

            // Act
            var result = await _controller.GetPurchaseOrderDetailById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePurchaseOrderDetail_ReturnsCreatedAtActionResult_WhenPurchaseOrderDetailIsValid()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { Quantity = 10 };
            ReflectionHelper.SetPropertyValue(purchaseOrderDetail, "Id", 1);
            _mockService.Setup(service => service.CreatePurchaseOrderDetailAsync(purchaseOrderDetail)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreatePurchaseOrderDetail(purchaseOrderDetail);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult);

            Assert.Equal("GetPurchaseOrderDetailById", createdResult.ActionName);
            Assert.NotNull(createdResult.RouteValues);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdatePurchaseOrderDetail_ReturnsNoContent_WhenPurchaseOrderDetailIsValid()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { Quantity = 10 };
            ReflectionHelper.SetPropertyValue(purchaseOrderDetail, "Id", 1);
            _mockService.Setup(service => service.UpdatePurchaseOrderDetailAsync(purchaseOrderDetail)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePurchaseOrderDetail(1, purchaseOrderDetail);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePurchaseOrderDetail_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { Quantity = 10 };
            ReflectionHelper.SetPropertyValue(purchaseOrderDetail, "Id", 2);

            // Act
            var result = await _controller.UpdatePurchaseOrderDetail(1, purchaseOrderDetail);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePurchaseOrderDetail_ReturnsNoContent_WhenPurchaseOrderDetailIsDeleted()
        {
            // Arrange
            _mockService.Setup(service => service.DeletePurchaseOrderDetailAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePurchaseOrderDetail(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetPurchaseOrderDetailsByOrderId_ReturnsOkResult_WithPurchaseOrderDetails()
        {
            // Arrange
            var purchaseOrderDetails = new List<PurchaseOrderDetail> { new PurchaseOrderDetail { Quantity = 10 } };
            _mockService.Setup(service => service.GetPurchaseOrderDetailsByOrderIdAsync(1)).ReturnsAsync(purchaseOrderDetails);

            // Act
            var result = await _controller.GetPurchaseOrderDetailsByOrderId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPurchaseOrderDetails = Assert.IsType<List<PurchaseOrderDetail>>(okResult.Value);
            Assert.Single(returnPurchaseOrderDetails);
        }
    }
}
