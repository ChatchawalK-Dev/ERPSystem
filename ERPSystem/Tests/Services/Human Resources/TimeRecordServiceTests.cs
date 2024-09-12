using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using ERPSystem.Data.Repository.HumanResources;

namespace ERPSystem.Tests.Services.HumanResources
{
    public class TimeRecordServiceTests
    {
        private readonly Mock<ITimeRecordRepository> _mockRepository;
        private readonly ITimeRecordService _service;

        public TimeRecordServiceTests()
        {
            _mockRepository = new Mock<ITimeRecordRepository>();
            _service = new TimeRecordService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllTimeRecordsAsync_ReturnsAllTimeRecords()
        {
            // Arrange
            var timeRecords = new List<TimeRecord>
            {
                new TimeRecord { HoursWorked = 8, Date = DateTime.Now },
                new TimeRecord { HoursWorked = 7, Date = DateTime.Now.AddDays(-1) }
            };
            ReflectionHelper.SetPropertyValue(timeRecords[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(timeRecords[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(timeRecords);

            // Act
            var result = await _service.GetAllTimeRecordsAsync();

            // Assert
            Assert.NotNull(result);
            var timeRecordList = result.ToList();
            Assert.Equal(2, timeRecordList.Count);
            Assert.All(timeRecordList, timeRecord =>
            {
                Assert.NotNull(timeRecord);
                Assert.True(timeRecord.HoursWorked > 0);
                Assert.NotEqual(default(DateTime), timeRecord.Date);
            });
        }

        [Fact]
        public async Task GetTimeRecordByIdAsync_ReturnsTimeRecord()
        {
            // Arrange
            var timeRecord = new TimeRecord { HoursWorked = 8, Date = DateTime.Now };
            ReflectionHelper.SetPropertyValue(timeRecord, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(timeRecord);

            // Act
            var result = await _service.GetTimeRecordByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(8, result?.HoursWorked);
            Assert.NotEqual(default(DateTime), result?.Date);
        }

        [Fact]
        public async Task CreateTimeRecordAsync_CreatesTimeRecord()
        {
            // Arrange
            var timeRecord = new TimeRecord { HoursWorked = 8, Date = DateTime.Now };

            // Act
            await _service.CreateTimeRecordAsync(timeRecord);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(timeRecord), Times.Once);
        }

        [Fact]
        public async Task UpdateTimeRecordAsync_UpdatesTimeRecord()
        {
            // Arrange
            var timeRecord = new TimeRecord { HoursWorked = 8, Date = DateTime.Now };

            // Act
            await _service.UpdateTimeRecordAsync(timeRecord);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(timeRecord), Times.Once);
        }

        [Fact]
        public async Task DeleteTimeRecordAsync_DeletesTimeRecord()
        {
            // Act
            await _service.DeleteTimeRecordAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetTimeRecordsByEmployeeIdAsync_ReturnsTimeRecordsByEmployeeId()
        {
            // Arrange
            var timeRecords = new List<TimeRecord>
            {
                new TimeRecord { HoursWorked = 8, Date = DateTime.Now },
                new TimeRecord { HoursWorked = 7, Date = DateTime.Now.AddDays(-1) }
            };
            ReflectionHelper.SetPropertyValue(timeRecords[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(timeRecords[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetTimeRecordsByEmployeeIdAsync(1)).ReturnsAsync(timeRecords);

            // Act
            var result = await _service.GetTimeRecordsByEmployeeIdAsync(1);

            // Assert
            Assert.NotNull(result);
            var timeRecordList = result.ToList();
            Assert.Equal(2, timeRecordList.Count);
            Assert.All(timeRecordList, timeRecord =>
            {
                Assert.NotNull(timeRecord);
                Assert.True(timeRecord.HoursWorked > 0);
                Assert.NotEqual(default(DateTime), timeRecord.Date);
            });
        }
    }
}
