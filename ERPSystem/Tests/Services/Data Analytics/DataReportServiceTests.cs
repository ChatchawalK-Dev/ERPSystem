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
    public class DataReportServiceTests
    {
        private readonly Mock<IDataReportRepository> _mockRepository;
        private readonly DataReportService _service;

        public DataReportServiceTests()
        {
            _mockRepository = new Mock<IDataReportRepository>();
            _service = new DataReportService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetDataReportByIdAsync_ReturnsDataReport()
        {
            // Arrange
            var dataReport = new DataReport { ReportName = "Annual Report" };
            ReflectionHelper.SetPropertyValue(dataReport, "Id", 1);

            _mockRepository.Setup(repo => repo.GetDataReportByIdAsync(1)).ReturnsAsync(dataReport);

            // Act
            var result = await _service.GetDataReportByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Annual Report", result.ReportName);
        }

        [Fact]
        public async Task GetAllDataReportsAsync_ReturnsAllDataReports()
        {
            // Arrange
            var dataReports = new List<DataReport>
            {
                new DataReport { ReportName = "Annual Report" },
                new DataReport { ReportName = "Monthly Report" }
            };

            // Set the Id property using ReflectionHelper
            ReflectionHelper.SetPropertyValue(dataReports[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(dataReports[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllDataReportsAsync()).ReturnsAsync(dataReports);

            // Act
            var result = await _service.GetAllDataReportsAsync();

            // Assert
            Assert.NotNull(result);
            var reportList = result.ToList();
            Assert.Equal(2, reportList.Count);
            Assert.Contains(reportList, r => r.Id == 1 && r.ReportName == "Annual Report");
            Assert.Contains(reportList, r => r.Id == 2 && r.ReportName == "Monthly Report");
        }

        [Fact]
        public async Task AddDataReportAsync_CallsRepositoryAdd()
        {
            // Arrange
            var dataReport = new DataReport { ReportName = "Annual Report" };
            ReflectionHelper.SetPropertyValue(dataReport, "Id", 1);

            // Act
            await _service.AddDataReportAsync(dataReport);

            // Assert
            _mockRepository.Verify(repo => repo.AddDataReportAsync(It.IsAny<DataReport>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDataReportAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var dataReport = new DataReport { ReportName = "Annual Report" };
            ReflectionHelper.SetPropertyValue(dataReport, "Id", 1);

            // Act
            await _service.UpdateDataReportAsync(dataReport);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateDataReportAsync(It.IsAny<DataReport>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDataReportAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteDataReportAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteDataReportAsync(id), Times.Once);
        }
    }
}
