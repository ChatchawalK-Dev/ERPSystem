using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services;
using ERPSystem.Repositories;

namespace ERPSystem.Tests.Services.Data_Analytics
{
    public class DashboardServiceTests
    {
        private readonly Mock<IDashboardRepository> _mockRepository;
        private readonly DashboardService _service;

        public DashboardServiceTests()
        {
            _mockRepository = new Mock<IDashboardRepository>();
            _service = new DashboardService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllDashboardsAsync_ReturnsAllDashboards()
        {
            // Arrange
            var dashboards = new List<Dashboard>
            {
                new Dashboard { DashboardName = "Sales Overview" },
                new Dashboard { DashboardName = "Revenue Trends" }
            };

            // Set the Id property using ReflectionHelper
            ReflectionHelper.SetPropertyValue(dashboards[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(dashboards[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllDashboardsAsync()).ReturnsAsync(dashboards);

            // Act
            var result = await _service.GetAllDashboardsAsync();

            // Assert
            Assert.NotNull(result);
            var dashboardList = result.ToList();
            Assert.Equal(2, dashboardList.Count);
            Assert.Contains(dashboardList, d => d.Id == 1 && d.DashboardName == "Sales Overview");
            Assert.Contains(dashboardList, d => d.Id == 2 && d.DashboardName == "Revenue Trends");
        }

        [Fact]
        public async Task GetDashboardByIdAsync_ReturnsDashboard()
        {
            // Arrange
            var dashboard = new Dashboard { DashboardName = "Sales Overview" };
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);

            _mockRepository.Setup(repo => repo.GetDashboardByIdAsync(1)).ReturnsAsync(dashboard);

            // Act
            var result = await _service.GetDashboardByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Sales Overview", result.DashboardName);
        }

        [Fact]
        public async Task AddDashboardAsync_CallsRepositoryAdd()
        {
            // Arrange
            var dashboard = new Dashboard { DashboardName = "Sales Overview" };
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);

            // Act
            await _service.AddDashboardAsync(dashboard);

            // Assert
            _mockRepository.Verify(repo => repo.AddDashboardAsync(It.IsAny<Dashboard>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDashboardAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var dashboard = new Dashboard { DashboardName = "Sales Overview" };
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);

            // Act
            await _service.UpdateDashboardAsync(dashboard);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateDashboardAsync(It.IsAny<Dashboard>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDashboardAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteDashboardAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteDashboardAsync(id), Times.Once);
        }
    }
}
