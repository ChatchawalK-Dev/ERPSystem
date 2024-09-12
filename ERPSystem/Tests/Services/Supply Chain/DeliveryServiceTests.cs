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
    public class DeliveryServiceTests
    {
        private readonly Mock<IDeliveryRepository> _mockRepository;
        private readonly DeliveryService _service;

        public DeliveryServiceTests()
        {
            _mockRepository = new Mock<IDeliveryRepository>();
            _service = new DeliveryService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllDeliveriesAsync_ReturnsAllDeliveries()
        {
            // Arrange
            var deliveries = new List<Delivery>
            {
                new Delivery { /* Initialize properties */ },
                new Delivery { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(deliveries[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(deliveries[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(deliveries);

            // Act
            var result = await _service.GetAllDeliveriesAsync();

            // Assert
            Assert.NotNull(result);
            var deliveryList = result.ToList();
            Assert.Equal(2, deliveryList.Count);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(deliveryList[0], "Id"));
            Assert.Equal(2, ReflectionHelper.GetPropertyValue(deliveryList[1], "Id"));
        }

        [Fact]
        public async Task GetDeliveryByIdAsync_ReturnsCorrectDelivery()
        {
            // Arrange
            var delivery = new Delivery { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(delivery, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(delivery);

            // Act
            var result = await _service.GetDeliveryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreateDeliveryAsync_CallsRepositoryAdd()
        {
            // Arrange
            var delivery = new Delivery { /* Initialize properties */ };

            // Act
            await _service.CreateDeliveryAsync(delivery);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(delivery), Times.Once);
        }

        [Fact]
        public async Task UpdateDeliveryAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var delivery = new Delivery { /* Initialize properties */ };

            // Act
            await _service.UpdateDeliveryAsync(delivery);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(delivery), Times.Once);
        }

        [Fact]
        public async Task DeleteDeliveryAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteDeliveryAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetDeliveriesByPurchaseOrderIdAsync_ReturnsDeliveries()
        {
            // Arrange
            var deliveries = new List<Delivery>
            {
                new Delivery { /* Initialize properties */ },
                new Delivery { /* Initialize properties */ }
            };
            int purchaseOrderId = 1;
            _mockRepository.Setup(repo => repo.GetDeliveriesByPurchaseOrderIdAsync(purchaseOrderId))
                .ReturnsAsync(deliveries);

            // Act
            var result = await _service.GetDeliveriesByPurchaseOrderIdAsync(purchaseOrderId);

            // Assert
            Assert.NotNull(result);
            var deliveryList = result.ToList();
            Assert.Equal(2, deliveryList.Count);
            // Optionally check specific properties if needed
        }
    }
}
