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
    public class PurchaseOrderDetailServiceTests
    {
        private readonly Mock<IPurchaseOrderDetailRepository> _mockRepository;
        private readonly PurchaseOrderDetailService _service;

        public PurchaseOrderDetailServiceTests()
        {
            _mockRepository = new Mock<IPurchaseOrderDetailRepository>();
            _service = new PurchaseOrderDetailService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllPurchaseOrderDetailsAsync_ReturnsAllPurchaseOrderDetails()
        {
            // Arrange
            var purchaseOrderDetails = new List<PurchaseOrderDetail>
            {
                new PurchaseOrderDetail { /* Initialize properties */ },
                new PurchaseOrderDetail { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(purchaseOrderDetails[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(purchaseOrderDetails[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(purchaseOrderDetails);

            // Act
            var result = await _service.GetAllPurchaseOrderDetailsAsync();

            // Assert
            Assert.NotNull(result);
            var detailList = result.ToList();
            Assert.Equal(2, detailList.Count);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(detailList[0], "Id"));
            Assert.Equal(2, ReflectionHelper.GetPropertyValue(detailList[1], "Id"));
        }

        [Fact]
        public async Task GetPurchaseOrderDetailByIdAsync_ReturnsCorrectPurchaseOrderDetail()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(purchaseOrderDetail, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(purchaseOrderDetail);

            // Act
            var result = await _service.GetPurchaseOrderDetailByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreatePurchaseOrderDetailAsync_CallsRepositoryAdd()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { /* Initialize properties */ };

            // Act
            await _service.CreatePurchaseOrderDetailAsync(purchaseOrderDetail);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(purchaseOrderDetail), Times.Once);
        }

        [Fact]
        public async Task UpdatePurchaseOrderDetailAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var purchaseOrderDetail = new PurchaseOrderDetail { /* Initialize properties */ };

            // Act
            await _service.UpdatePurchaseOrderDetailAsync(purchaseOrderDetail);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(purchaseOrderDetail), Times.Once);
        }

        [Fact]
        public async Task DeletePurchaseOrderDetailAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeletePurchaseOrderDetailAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetPurchaseOrderDetailsByOrderIdAsync_ReturnsPurchaseOrderDetails()
        {
            // Arrange
            var purchaseOrderDetails = new List<PurchaseOrderDetail>
            {
                new PurchaseOrderDetail { /* Initialize properties */ },
                new PurchaseOrderDetail { /* Initialize properties */ }
            };
            int orderId = 1;
            _mockRepository.Setup(repo => repo.GetPurchaseOrderDetailsByOrderIdAsync(orderId))
                .ReturnsAsync(purchaseOrderDetails);

            // Act
            var result = await _service.GetPurchaseOrderDetailsByOrderIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            var detailList = result.ToList();
            Assert.Equal(2, detailList.Count);
            // Optionally check specific properties if needed
        }
    }
}
