using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using ERPSystem.Data.Repository.SupplyChain;

namespace ERPSystem.Tests.Services.SupplyChain
{
    public class PurchaseOrderServiceTests
    {
        private readonly Mock<IPurchaseOrderRepository> _mockRepository;
        private readonly PurchaseOrderService _service;

        public PurchaseOrderServiceTests()
        {
            _mockRepository = new Mock<IPurchaseOrderRepository>();
            _service = new PurchaseOrderService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllPurchaseOrdersAsync_ReturnsAllPurchaseOrders()
        {
            // Arrange
            var purchaseOrders = new List<PurchaseOrder>
            {
                new PurchaseOrder { /* Initialize properties */ },
                new PurchaseOrder { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(purchaseOrders[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(purchaseOrders[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(purchaseOrders);

            // Act
            var result = await _service.GetAllPurchaseOrdersAsync();

            // Assert
            Assert.NotNull(result);
            var orderList = result.ToList();
            Assert.Equal(2, orderList.Count);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(orderList[0], "Id"));
            Assert.Equal(2, ReflectionHelper.GetPropertyValue(orderList[1], "Id"));
        }

        [Fact]
        public async Task GetPurchaseOrderByIdAsync_ReturnsCorrectPurchaseOrder()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(purchaseOrder, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(purchaseOrder);

            // Act
            var result = await _service.GetPurchaseOrderByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreatePurchaseOrderAsync_CallsRepositoryAdd()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { /* Initialize properties */ };

            // Act
            await _service.CreatePurchaseOrderAsync(purchaseOrder);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(purchaseOrder), Times.Once);
        }

        [Fact]
        public async Task UpdatePurchaseOrderAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder { /* Initialize properties */ };

            // Act
            await _service.UpdatePurchaseOrderAsync(purchaseOrder);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(purchaseOrder), Times.Once);
        }

        [Fact]
        public async Task DeletePurchaseOrderAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeletePurchaseOrderAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetPurchaseOrdersBySupplierIdAsync_ReturnsPurchaseOrders()
        {
            // Arrange
            var purchaseOrders = new List<PurchaseOrder>
            {
                new PurchaseOrder { /* Initialize properties */ },
                new PurchaseOrder { /* Initialize properties */ }
            };
            int supplierId = 1;
            _mockRepository.Setup(repo => repo.GetPurchaseOrdersBySupplierIdAsync(supplierId))
                .ReturnsAsync(purchaseOrders);

            // Act
            var result = await _service.GetPurchaseOrdersBySupplierIdAsync(supplierId);

            // Assert
            Assert.NotNull(result);
            var orderList = result.ToList();
            Assert.Equal(2, orderList.Count);
            // Optionally check specific properties if needed
        }
    }
}
