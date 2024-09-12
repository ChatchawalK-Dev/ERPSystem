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
    public class SalesOrderServiceTests
    {
        private readonly Mock<ISalesOrderRepository> _mockRepository;
        private readonly SalesOrderService _service;

        public SalesOrderServiceTests()
        {
            _mockRepository = new Mock<ISalesOrderRepository>();
            _service = new SalesOrderService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllSalesOrdersAsync_ReturnsAllSalesOrders()
        {
            // Arrange
            var salesOrders = new List<SalesOrder>
            {
                new SalesOrder { TotalAmount = 100 },
                new SalesOrder { TotalAmount = 100 }
            };

            ReflectionHelper.SetPropertyValue(salesOrders[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(salesOrders[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(salesOrders);

            // Act
            var result = await _service.GetAllSalesOrdersAsync();

            // Assert
            Assert.NotNull(result);
            var orderList = result.ToList();
            Assert.Equal(2, orderList.Count);
            Assert.Contains(orderList, o => o.Id == 1 && o.TotalAmount == 100);
            Assert.Contains(orderList, o => o.Id == 2 && o.TotalAmount == 100);
        }

        [Fact]
        public async Task GetSalesOrderByIdAsync_ReturnsSalesOrder()
        {
            // Arrange
            var salesOrder = new SalesOrder { TotalAmount = 100 };
            ReflectionHelper.SetPropertyValue(salesOrder, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(salesOrder);

            // Act
            var result = await _service.GetSalesOrderByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(100, result.TotalAmount);
        }

        [Fact]
        public async Task CreateSalesOrderAsync_CallsRepositoryAdd()
        {
            // Arrange
            var salesOrder = new SalesOrder { TotalAmount = 100 };
            ReflectionHelper.SetPropertyValue(salesOrder, "Id", 1);

            // Act
            await _service.CreateSalesOrderAsync(salesOrder);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<SalesOrder>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSalesOrderAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var salesOrder = new SalesOrder { TotalAmount = 100 };
            ReflectionHelper.SetPropertyValue(salesOrder, "Id", 1);

            // Act
            await _service.UpdateSalesOrderAsync(salesOrder);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<SalesOrder>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSalesOrderAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteSalesOrderAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetSalesOrdersByCustomerIdAsync_ReturnsSalesOrdersByCustomerId()
        {
            // Arrange
            var salesOrders = new List<SalesOrder>
            {
                new SalesOrder { TotalAmount = 100, CustomerID = 1 },
                new SalesOrder { TotalAmount = 100, CustomerID = 1 }
            };

            ReflectionHelper.SetPropertyValue(salesOrders[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(salesOrders[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetSalesOrdersByCustomerIdAsync(1)).ReturnsAsync(salesOrders);

            // Act
            var result = await _service.GetSalesOrdersByCustomerIdAsync(1);

            // Assert
            Assert.NotNull(result);
            var orderList = result.ToList();
            Assert.Equal(2, orderList.Count);
            Assert.Contains(orderList, o => o.Id == 1 && o.TotalAmount == 100);
            Assert.Contains(orderList, o => o.Id == 2 && o.TotalAmount == 100);
        }
    }
}
