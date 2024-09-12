using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using ERPSystem.Data.Repository.Cr;

namespace ERPSystem.Tests.Services.Cr
{
    public class SalesOrderDetailServiceTests
    {
        private readonly Mock<ISalesOrderDetailRepository> _mockRepository;
        private readonly SalesOrderDetailService _service;

        public SalesOrderDetailServiceTests()
        {
            _mockRepository = new Mock<ISalesOrderDetailRepository>();
            _service = new SalesOrderDetailService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllSalesOrderDetailsAsync_ReturnsAllSalesOrderDetails()
        {
            // Arrange
            var salesOrderDetails = new List<SalesOrderDetail>
            {
                new SalesOrderDetail { Quantity = 100 },
                new SalesOrderDetail { Quantity = 100 }
            };

            ReflectionHelper.SetPropertyValue(salesOrderDetails[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(salesOrderDetails[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(salesOrderDetails);

            // Act
            var result = await _service.GetAllSalesOrderDetailsAsync();

            // Assert
            Assert.NotNull(result);
            var detailList = result.ToList();
            Assert.Equal(2, detailList.Count);
            Assert.Contains(detailList, d => d.Id == 1 && d. Quantity == 100 );
            Assert.Contains(detailList, d => d.Id == 2 && d. Quantity == 100 );
        }

        [Fact]
        public async Task GetSalesOrderDetailByIdAsync_ReturnsSalesOrderDetail()
        {
            // Arrange
            var salesOrderDetail = new SalesOrderDetail { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(salesOrderDetail, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(salesOrderDetail);

            // Act
            var result = await _service.GetSalesOrderDetailByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(100, result.Quantity);
        }

        [Fact]
        public async Task CreateSalesOrderDetailAsync_CallsRepositoryAdd()
        {
            // Arrange
            var salesOrderDetail = new SalesOrderDetail { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(salesOrderDetail, "Id", 3);

            // Act
            await _service.CreateSalesOrderDetailAsync(salesOrderDetail);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<SalesOrderDetail>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSalesOrderDetailAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var salesOrderDetail = new SalesOrderDetail { Quantity = 100 };
            ReflectionHelper.SetPropertyValue(salesOrderDetail, "Id", 3);

            // Act
            await _service.UpdateSalesOrderDetailAsync(salesOrderDetail);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<SalesOrderDetail>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSalesOrderDetailAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 3;

            // Act
            await _service.DeleteSalesOrderDetailAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
