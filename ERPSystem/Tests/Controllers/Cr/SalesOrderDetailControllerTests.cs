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
    public class SalesOrderDetailControllerTests
    {
        private readonly Mock<ISalesOrderDetailService> _mockSalesOrderDetailService;
        private readonly SalesOrderDetailController _controller;

        public SalesOrderDetailControllerTests()
        {
            _mockSalesOrderDetailService = new Mock<ISalesOrderDetailService>();
            _controller = new SalesOrderDetailController(_mockSalesOrderDetailService.Object);
        }

        [Fact]
        public async Task GetSalesOrderDetails_ReturnsOkResult_WithListOfSalesOrderDetails()
        {
            // Arrange
            var salesOrderDetails = new List<SalesOrderDetail>
            {
                new SalesOrderDetail { /* Properties can be set here */ },
                new SalesOrderDetail { /* Properties can be set here */ }
            };
            _mockSalesOrderDetailService.Setup(service => service.GetAllSalesOrderDetailsAsync())
                                        .ReturnsAsync(salesOrderDetails);

            // Act
            var result = await _controller.GetSalesOrderDetails();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SalesOrderDetail>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SalesOrderDetail>>(okResult.Value);
            Assert.Equal(salesOrderDetails.Count, ((List<SalesOrderDetail>)returnValue).Count);
        }

        [Fact]
        public async Task GetSalesOrderDetail_ReturnsOkResult_WithSalesOrderDetail()
        {
            // Arrange
            var salesOrderDetailId = 1;
            var salesOrderDetail = new SalesOrderDetail { /* Set properties */ };
            ReflectionHelper.SetProperty(salesOrderDetail, "Id", salesOrderDetailId);

            _mockSalesOrderDetailService.Setup(service => service.GetSalesOrderDetailByIdAsync(salesOrderDetailId))
                                        .ReturnsAsync(salesOrderDetail);

            // Act
            var result = await _controller.GetSalesOrderDetail(salesOrderDetailId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SalesOrderDetail>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<SalesOrderDetail>(okResult.Value);
            Assert.Equal(salesOrderDetailId, returnValue.Id);
        }

        [Fact]
        public async Task CreateSalesOrderDetail_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var salesOrderDetail = new SalesOrderDetail { /* Set properties */ };
            ReflectionHelper.SetProperty(salesOrderDetail, "Id", 1);

            _mockSalesOrderDetailService.Setup(service => service.CreateSalesOrderDetailAsync(salesOrderDetail))
                                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateSalesOrderDetail(salesOrderDetail);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SalesOrderDetail>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<SalesOrderDetail>(createdAtActionResult.Value);
            Assert.Equal(salesOrderDetail.Id, returnValue.Id);
            Assert.Equal(nameof(SalesOrderDetailController.GetSalesOrderDetail), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateSalesOrderDetail_ReturnsNoContentResult()
        {
            // Arrange
            var salesOrderDetail = new SalesOrderDetail { /* Set properties */ };
            ReflectionHelper.SetProperty(salesOrderDetail, "Id", 1);

            _mockSalesOrderDetailService.Setup(service => service.UpdateSalesOrderDetailAsync(salesOrderDetail))
                                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateSalesOrderDetail(salesOrderDetail.Id, salesOrderDetail);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSalesOrderDetail_ReturnsNoContentResult()
        {
            // Arrange
            var salesOrderDetailId = 1;
            var salesOrderDetail = new SalesOrderDetail { /* Set properties */ };
            ReflectionHelper.SetProperty(salesOrderDetail, "Id", salesOrderDetailId);

            _mockSalesOrderDetailService.Setup(service => service.GetSalesOrderDetailByIdAsync(salesOrderDetailId))
                                        .ReturnsAsync(salesOrderDetail);

            _mockSalesOrderDetailService.Setup(service => service.DeleteSalesOrderDetailAsync(salesOrderDetailId))
                                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteSalesOrderDetail(salesOrderDetailId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
