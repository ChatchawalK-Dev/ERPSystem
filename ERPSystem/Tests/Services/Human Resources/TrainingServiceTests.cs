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
    public class TrainingServiceTests
    {
        private readonly Mock<ITrainingRepository> _mockRepository;
        private readonly ITrainingService _service;

        public TrainingServiceTests()
        {
            _mockRepository = new Mock<ITrainingRepository>();
            _service = new TrainingService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllTrainingsAsync_ReturnsAllTrainings()
        {
            // Arrange
            var trainings = new List<Training>
            {
                new Training { TrainingName = "Training 1", Date = DateTime.Now },
                new Training { TrainingName = "Training 2", Date = DateTime.Now.AddDays(-1) }
            };
            ReflectionHelper.SetPropertyValue(trainings[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(trainings[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trainings);

            // Act
            var result = await _service.GetAllTrainingsAsync();

            // Assert
            Assert.NotNull(result);
            var trainingList = result.ToList();
            Assert.Equal(2, trainingList.Count);
            Assert.All(trainingList, training =>
            {
                Assert.NotNull(training);
                Assert.False(string.IsNullOrEmpty(training.TrainingName));
                Assert.NotEqual(default(DateTime), training.Date);
            });
        }

        [Fact]
        public async Task GetTrainingByIdAsync_ReturnsTraining()
        {
            // Arrange
            var training = new Training { TrainingName = "Training 1", Date = DateTime.Now };
            ReflectionHelper.SetPropertyValue(training, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(training);

            // Act
            var result = await _service.GetTrainingByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Training 1", result?.TrainingName);
            Assert.NotEqual(default(DateTime), result?.Date);
        }

        [Fact]
        public async Task CreateTrainingAsync_CreatesTraining()
        {
            // Arrange
            var training = new Training { TrainingName = "Training 1", Date = DateTime.Now };

            // Act
            await _service.CreateTrainingAsync(training);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(training), Times.Once);
        }

        [Fact]
        public async Task UpdateTrainingAsync_UpdatesTraining()
        {
            // Arrange
            var training = new Training { TrainingName = "Training 1", Date = DateTime.Now };

            // Act
            await _service.UpdateTrainingAsync(training);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(training), Times.Once);
        }

        [Fact]
        public async Task DeleteTrainingAsync_DeletesTraining()
        {
            // Act
            await _service.DeleteTrainingAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetTrainingsByEmployeeIdAsync_ReturnsTrainingsByEmployeeId()
        {
            // Arrange
            var trainings = new List<Training>
            {
                new Training { TrainingName = "Training 1", Date = DateTime.Now },
                new Training { TrainingName = "Training 2", Date = DateTime.Now.AddDays(-1) }
            };
            ReflectionHelper.SetPropertyValue(trainings[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(trainings[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetTrainingsByEmployeeIdAsync(1)).ReturnsAsync(trainings);

            // Act
            var result = await _service.GetTrainingsByEmployeeIdAsync(1);

            // Assert
            Assert.NotNull(result);
            var trainingList = result.ToList();
            Assert.Equal(2, trainingList.Count);
            Assert.All(trainingList, training =>
            {
                Assert.NotNull(training);
                Assert.False(string.IsNullOrEmpty(training.TrainingName));
                Assert.NotEqual(default(DateTime), training.Date);
            });
        }
    }
}
