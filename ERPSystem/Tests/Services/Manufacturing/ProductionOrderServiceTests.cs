using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using ERPSystem.Data.Repository;

namespace ERPSystem.Tests.Services.Manufacturing
{
    public class ProductionOrderServiceTests
    {
        private readonly Mock<IRepository<ProductionOrder>> _mockRepository;
        private readonly ProductionOrderService _service;

        public ProductionOrderServiceTests()
        {
            _mockRepository = new Mock<IRepository<ProductionOrder>>();
            _service = new ProductionOrderService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProductionOrders()
        {
            // Arrange
            var productionOrders = new List<ProductionOrder>
            {
                new ProductionOrder { /* Initialize properties */ },
                new ProductionOrder { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(productionOrders[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(productionOrders[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productionOrders);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var ordersList = result.ToList();
            Assert.Equal(2, ordersList.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectProductionOrder()
        {
            // Arrange
            var productionOrder = new ProductionOrder { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(productionOrder, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(productionOrder);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAdd()
        {
            // Arrange
            var productionOrder = new ProductionOrder { /* Initialize properties */ };

            // Act
            await _service.AddAsync(productionOrder);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(productionOrder), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var productionOrder = new ProductionOrder { /* Initialize properties */ };

            // Act
            await _service.UpdateAsync(productionOrder);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(productionOrder), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
