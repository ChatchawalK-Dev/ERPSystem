using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using ERPSystem.Controllers.Data_Analytics;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services;

namespace ERPSystem.Tests.Controllers.Data_Analytics
{
    public class DataReportControllerTests
    {
        private readonly DataReportController _controller;
        private readonly Mock<IDataReportService> _mockDataReportService;

        public DataReportControllerTests()
        {
            _mockDataReportService = new Mock<IDataReportService>();
            _controller = new DataReportController(_mockDataReportService.Object);
        }

        [Fact]
        public async Task GetDataReport_ReturnsOkResult_WithDataReport()
        {
            // Arrange
            var dataReport = new DataReport();
            ReflectionHelper.SetPropertyValue(dataReport, "Id", 1);

            _mockDataReportService.Setup(service => service.GetDataReportByIdAsync(1))
                                  .ReturnsAsync(dataReport);

            // Act
            var result = await _controller.GetDataReport(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<DataReport>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedDataReport = Assert.IsAssignableFrom<DataReport>(okResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnedDataReport, "Id"));
        }

        [Fact]
        public async Task GetAllDataReports_ReturnsOkResult_WithDataReports()
        {
            // Arrange
            var dataReports = new List<DataReport>
            {
                new DataReport(),
                new DataReport()
            };
            ReflectionHelper.SetPropertyValue(dataReports[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(dataReports[1], "Id", 2);

            _mockDataReportService.Setup(service => service.GetAllDataReportsAsync())
                                  .ReturnsAsync(dataReports);

            // Act
            var result = await _controller.GetAllDataReports();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<DataReport>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedDataReports = Assert.IsAssignableFrom<IEnumerable<DataReport>>(okResult.Value);

            Assert.Equal(dataReports.Count, returnedDataReports.Count());
            foreach (var dataReport in dataReports)
            {
                var matchedReport = returnedDataReports.FirstOrDefault(d => d.Id == dataReport.Id);
                Assert.NotNull(matchedReport);
            }
        }

        [Fact]
        public async Task CreateDataReport_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var dataReport = new DataReport();
            ReflectionHelper.SetPropertyValue(dataReport, "Id", 1);

            _mockDataReportService.Setup(service => service.AddDataReportAsync(dataReport))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateDataReport(dataReport);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<DataReport>>(result);
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<DataReport>(createdAtActionResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnValue, "Id"));
            Assert.Equal(nameof(DataReportController.GetDataReport), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateDataReport_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var dataReport = new DataReport();
            ReflectionHelper.SetPropertyValue(dataReport, "Id", 1);

            _mockDataReportService.Setup(service => service.GetDataReportByIdAsync(1))
                                  .ReturnsAsync(dataReport);

            _mockDataReportService.Setup(service => service.UpdateDataReportAsync(dataReport))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateDataReport(1, dataReport);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteDataReport_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockDataReportService.Setup(service => service.DeleteDataReportAsync(1))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteDataReport(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
