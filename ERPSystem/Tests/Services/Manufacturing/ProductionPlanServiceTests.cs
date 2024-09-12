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
    public class ProductionPlanServiceTests
    {
        private readonly Mock<IRepository<ProductionPlan>> _mockRepository;
        private readonly ProductionPlanService _service;

        public ProductionPlanServiceTests()
        {
            _mockRepository = new Mock<IRepository<ProductionPlan>>();
            _service = new ProductionPlanService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProductionPlans()
        {
            // Arrange
            var productionPlans = new List<ProductionPlan>
            {
                new ProductionPlan { /* Initialize properties */ },
                new ProductionPlan { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(productionPlans[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(productionPlans[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productionPlans);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var plansList = result.ToList();
            Assert.Equal(2, plansList.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectProductionPlan()
        {
            // Arrange
            var productionPlan = new ProductionPlan { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(productionPlan, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(productionPlan);

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
            var productionPlan = new ProductionPlan { /* Initialize properties */ };

            // Act
            await _service.AddAsync(productionPlan);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(productionPlan), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var productionPlan = new ProductionPlan { /* Initialize properties */ };

            // Act
            await _service.UpdateAsync(productionPlan);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(productionPlan), Times.Once);
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
