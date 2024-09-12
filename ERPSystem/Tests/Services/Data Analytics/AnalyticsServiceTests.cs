using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services.Data_Analytics;
using ERPSystem.Repositories;

namespace ERPSystem.Tests.Services.Data_Analytics
{
    public class AnalyticsServiceTests
    {
        private readonly Mock<IAnalyticsRepository> _mockRepository;
        private readonly AnalyticsService _service;

        public AnalyticsServiceTests()
        {
            _mockRepository = new Mock<IAnalyticsRepository>();
            _service = new AnalyticsService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsAnalytics()
        {
            // Arrange
            var analytics = new Analytics { AnalysisType = "Revenue" };
            ReflectionHelper.SetPropertyValue(analytics, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(analytics);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Revenue", result.AnalysisType);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAnalytics()
        {
            // Arrange
            var analyticsList = new List<Analytics>
            {
                new Analytics { AnalysisType = "Revenue" },
                new Analytics { AnalysisType = "Cost" }
            };

            ReflectionHelper.SetPropertyValue(analyticsList[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(analyticsList[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(analyticsList);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var analyticsListResult = result.ToList();
            Assert.Equal(2, analyticsListResult.Count);
            Assert.Contains(analyticsListResult, a => a.Id == 1 && a.AnalysisType == "Revenue");
            Assert.Contains(analyticsListResult, a => a.Id == 2 && a.AnalysisType == "Cost");
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAdd()
        {
            // Arrange
            var analytics = new Analytics { AnalysisType = "Revenue" };
            ReflectionHelper.SetPropertyValue(analytics, "Id", 1);

            // Act
            await _service.AddAsync(analytics);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Analytics>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var analytics = new Analytics { AnalysisType = "Revenue" };
            ReflectionHelper.SetPropertyValue(analytics, "Id", 1);

            // Act
            await _service.UpdateAsync(analytics);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Analytics>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
