using ERPSystem.Controllers.HumanResources;
using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.HumanResources
{
    public class TimeRecordControllerTests
    {
        private readonly Mock<ITimeRecordService> _mockTimeRecordService;
        private readonly TimeRecordController _controller;

        public TimeRecordControllerTests()
        {
            _mockTimeRecordService = new Mock<ITimeRecordService>();
            _controller = new TimeRecordController(_mockTimeRecordService.Object);
        }

        [Fact]
        public async Task GetAllTimeRecords_ReturnsOkResult_WithListOfTimeRecords()
        {
            // Arrange
            var timeRecords = new List<TimeRecord>
            {
                new TimeRecord { EmployeeID = 101, HoursWorked = 8 },
                new TimeRecord { EmployeeID = 102, HoursWorked = 7 }
            };
            ReflectionHelper.SetPropertyValue(timeRecords[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(timeRecords[1], "Id", 2);
            _mockTimeRecordService.Setup(service => service.GetAllTimeRecordsAsync())
                                  .ReturnsAsync(timeRecords);

            // Act
            var result = await _controller.GetAllTimeRecords();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTimeRecords = Assert.IsAssignableFrom<IEnumerable<TimeRecord>>(okResult.Value);
            Assert.Equal(2, ((List<TimeRecord>)returnedTimeRecords).Count);
        }

        [Fact]
        public async Task GetTimeRecordById_ReturnsOkResult_WithTimeRecord()
        {
            // Arrange
            var timeRecord = new TimeRecord { EmployeeID = 101, HoursWorked = 8 };
            ReflectionHelper.SetPropertyValue(timeRecord, "Id", 1);
            _mockTimeRecordService.Setup(service => service.GetTimeRecordByIdAsync(1))
                                  .ReturnsAsync(timeRecord);

            // Act
            var result = await _controller.GetTimeRecordById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTimeRecord = Assert.IsAssignableFrom<TimeRecord>(okResult.Value);
            Assert.Equal(1, returnedTimeRecord.Id);
        }

        [Fact]
        public async Task CreateTimeRecord_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var timeRecord = new TimeRecord { EmployeeID = 101, HoursWorked = 8 };
            ReflectionHelper.SetPropertyValue(timeRecord, "Id", 1);
            _mockTimeRecordService.Setup(service => service.CreateTimeRecordAsync(timeRecord))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateTimeRecord(timeRecord);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedTimeRecord = Assert.IsAssignableFrom<TimeRecord>(createdAtActionResult.Value);
            Assert.Equal(timeRecord.Id, returnedTimeRecord.Id);
            Assert.Equal(nameof(TimeRecordController.GetTimeRecordById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateTimeRecord_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var timeRecord = new TimeRecord { EmployeeID = 101, HoursWorked = 8 };
            ReflectionHelper.SetPropertyValue(timeRecord, "Id", 2);

            // Act
            var result = await _controller.UpdateTimeRecord(1, timeRecord);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteTimeRecord_ReturnsNoContentResult()
        {
            // Arrange
            _mockTimeRecordService.Setup(service => service.DeleteTimeRecordAsync(1))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTimeRecord(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetTimeRecordsByEmployeeId_ReturnsOkResult_WithListOfTimeRecords()
        {
            // Arrange
            var timeRecords = new List<TimeRecord>
            {
                new TimeRecord { EmployeeID = 101, HoursWorked = 8 },
                new TimeRecord { EmployeeID = 101, HoursWorked = 7 }
            };
            ReflectionHelper.SetPropertyValue(timeRecords[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(timeRecords[1], "Id", 2);
            _mockTimeRecordService.Setup(service => service.GetTimeRecordsByEmployeeIdAsync(101))
                                  .ReturnsAsync(timeRecords);

            // Act
            var result = await _controller.GetTimeRecordsByEmployeeId(101);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTimeRecords = Assert.IsAssignableFrom<IEnumerable<TimeRecord>>(okResult.Value);
            Assert.Equal(2, ((List<TimeRecord>)returnedTimeRecords).Count);
        }
    }
}
