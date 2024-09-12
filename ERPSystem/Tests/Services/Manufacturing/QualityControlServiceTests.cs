using System;
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
    public class QualityControlServiceTests
    {
        private readonly Mock<IRepository<QualityControl>> _mockRepository;
        private readonly QualityControlService _service;

        public QualityControlServiceTests()
        {
            _mockRepository = new Mock<IRepository<QualityControl>>();
            _service = new QualityControlService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllItemsAsync_ReturnsAllQualityControls()
        {
            // Arrange
            var qualityControls = new List<QualityControl>
            {
                new QualityControl { /* Initialize properties */ },
                new QualityControl { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(qualityControls[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(qualityControls[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(qualityControls);

            // Act
            var result = await _service.GetAllItemsAsync();

            // Assert
            Assert.NotNull(result);
            var controlsList = result.ToList();
            Assert.Equal(2, controlsList.Count);
        }

        [Fact]
        public async Task GetItemByIdAsync_ReturnsCorrectQualityControl()
        {
            // Arrange
            var qualityControl = new QualityControl { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(qualityControl, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(qualityControl);

            // Act
            var result = await _service.GetItemByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task AddItemAsync_CallsRepositoryAdd()
        {
            // Arrange
            var qualityControl = new QualityControl { /* Initialize properties */ };

            // Act
            await _service.AddItemAsync(qualityControl);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(qualityControl), Times.Once);
        }

        [Fact]
        public async Task UpdateItemAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var qualityControl = new QualityControl { /* Initialize properties */ };

            // Act
            await _service.UpdateItemAsync(qualityControl);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(qualityControl), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteItemAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task FindItemAsync_ReturnsItemMatchingPredicate()
        {
            // Arrange
            var qualityControls = new List<QualityControl>
            {
                new QualityControl { /* Initialize properties */ },
                new QualityControl { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(qualityControls[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(qualityControls[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(qualityControls);

            // Act
            var result = await _service.FindItemAsync(q => q.Id == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }
    }
}
