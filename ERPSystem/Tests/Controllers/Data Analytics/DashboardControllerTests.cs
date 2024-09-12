using ERPSystem.Controllers;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Data_Analytics
{
    public class DashboardControllerTests
    {
        private readonly Mock<IDashboardService> _mockDashboardService;
        private readonly DashboardController _controller;

        public DashboardControllerTests()
        {
            _mockDashboardService = new Mock<IDashboardService>();
            _controller = new DashboardController(_mockDashboardService.Object);
        }

        [Fact]
        public async Task GetDashboards_ReturnsOkResult_WithDashboards()
        {
            // Arrange
            var dashboards = new List<Dashboard>
            {
                new Dashboard (),
                new Dashboard ()
            };
            ReflectionHelper.SetPropertyValue(dashboards[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(dashboards[1], "Id", 2);
            _mockDashboardService.Setup(service => service.GetAllDashboardsAsync())
                                  .ReturnsAsync(dashboards);

            // Act
            var result = await _controller.GetDashboards();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Dashboard>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedDashboards = Assert.IsAssignableFrom<IEnumerable<Dashboard>>(okResult.Value);

            // Verify that the returned dashboards match the expected ones
            Assert.Equal(dashboards.Count(), returnedDashboards.Count());
            foreach (var dashboard in dashboards)
            {
                var matchedDashboard = returnedDashboards.FirstOrDefault(d => d.Id == dashboard.Id);
                Assert.NotNull(matchedDashboard);
            }
        }

        [Fact]
        public async Task GetDashboard_ReturnsOkResult_WithDashboard()
        {
            // Arrange
            var dashboard = new Dashboard ();
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);
            _mockDashboardService.Setup(service => service.GetDashboardByIdAsync(1))
                                  .ReturnsAsync(dashboard);

            // Act
            var result = await _controller.GetDashboard(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Dashboard>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedDashboard = Assert.IsAssignableFrom<Dashboard>(okResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnedDashboard, "Id"));
        }

        [Fact]
        public async Task GetDashboard_ReturnsNotFound_WhenDashboardNotFound()
        {
            // Arrange
            _mockDashboardService.Setup(service => service.GetDashboardByIdAsync(1))
                                  .ReturnsAsync((Dashboard?)null);

            // Act
            var result = await _controller.GetDashboard(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Dashboard>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateDashboard_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var dashboard = new Dashboard();
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);
            _mockDashboardService.Setup(service => service.AddDashboardAsync(dashboard))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateDashboard(dashboard);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Dashboard>>(result);
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<Dashboard>(createdAtActionResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnValue, "Id"));
            Assert.Equal(nameof(DashboardController.GetDashboard), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateDashboard_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var dashboard = new Dashboard();
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);

            // Ensure the mock returns a valid dashboard for the given ID
            _mockDashboardService.Setup(service => service.GetDashboardByIdAsync(1))
                                  .ReturnsAsync(dashboard);

            _mockDashboardService.Setup(service => service.UpdateDashboardAsync(dashboard))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateDashboard(1, dashboard);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateDashboard_ReturnsNotFound_WhenDashboardNotFound()
        {
            // Arrange
            var dashboard = new Dashboard ();
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);
            _mockDashboardService.Setup(service => service.GetDashboardByIdAsync(1))
                                  .ReturnsAsync((Dashboard?)null);

            // Act
            var result = await _controller.UpdateDashboard(1, dashboard);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteDashboard_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var dashboard = new Dashboard ();
            ReflectionHelper.SetPropertyValue(dashboard, "Id", 1);
            _mockDashboardService.Setup(service => service.GetDashboardByIdAsync(1))
                                  .ReturnsAsync(dashboard);
            _mockDashboardService.Setup(service => service.DeleteDashboardAsync(1))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteDashboard(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteDashboard_ReturnsNotFound_WhenDashboardNotFound()
        {
            // Arrange
            _mockDashboardService.Setup(service => service.GetDashboardByIdAsync(1))
                                  .ReturnsAsync((Dashboard?)null);

            // Act
            var result = await _controller.DeleteDashboard(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
