using ERPSystem.Controllers.Data_Analytics;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services.Data_Analytics;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Data_Analytics
{
    public class AnalyticsControllerTests
    {
        private readonly Mock<IAnalyticsService> _mockAnalyticsService;
        private readonly AnalyticsController _controller;

        public AnalyticsControllerTests()
        {
            _mockAnalyticsService = new Mock<IAnalyticsService>();
            _controller = new AnalyticsController(_mockAnalyticsService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithAnalytics()
        {
            // Arrange
            var analytics = new Analytics();

            // Use reflection to set the Id property
            ReflectionHelper.SetPropertyValue(analytics, "Id", 1);

            _mockAnalyticsService.Setup(service => service.GetByIdAsync(1))
                                  .ReturnsAsync(analytics);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Analytics>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedAnalytics = Assert.IsAssignableFrom<Analytics>(okResult.Value);

            // Verify the property value directly
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnedAnalytics, "Id"));
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAnalyticsNotFound()
        {
            // Arrange
            _mockAnalyticsService.Setup(service => service.GetByIdAsync(1))
                                  .ReturnsAsync((Analytics?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Analytics>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfAnalytics()
        {
            // Arrange
            var analyticsList = new List<Analytics>
            {
                new Analytics (),
                new Analytics ()
            };
            _mockAnalyticsService.Setup(service => service.GetAllAsync())
                                  .ReturnsAsync(analyticsList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Analytics>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedAnalyticsList = Assert.IsAssignableFrom<IEnumerable<Analytics>>(okResult.Value);
            Assert.Equal(analyticsList, returnedAnalyticsList);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var analytics = new Analytics();
            ReflectionHelper.SetPropertyValue(analytics, "Id", 1);

            _mockAnalyticsService.Setup(service => service.AddAsync(analytics))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(analytics);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Analytics>>(result);
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<Analytics>(createdAtActionResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnValue, "Id"));
            Assert.Equal(nameof(AnalyticsController.GetById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var analytics = new Analytics ();
            ReflectionHelper.SetPropertyValue(analytics, "Id", 1);
            _mockAnalyticsService.Setup(service => service.UpdateAsync(analytics))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(1, analytics);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockAnalyticsService.Setup(service => service.DeleteAsync(1))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
