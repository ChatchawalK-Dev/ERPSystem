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
    public class TrainingControllerTests
    {
        private readonly Mock<ITrainingService> _mockTrainingService;
        private readonly TrainingController _controller;

        public TrainingControllerTests()
        {
            _mockTrainingService = new Mock<ITrainingService>();
            _controller = new TrainingController(_mockTrainingService.Object);
        }

        [Fact]
        public async Task GetAllTrainings_ReturnsOkResult_WithListOfTrainings()
        {
            // Arrange
            var trainings = new List<Training>
            {
                new Training { EmployeeID = 101, TrainingName = "Safety Training" },
                new Training { EmployeeID = 102, TrainingName = "Leadership Training" }
            };
            ReflectionHelper.SetPropertyValue(trainings[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(trainings[1], "Id", 2);
            _mockTrainingService.Setup(service => service.GetAllTrainingsAsync())
                                .ReturnsAsync(trainings);

            // Act
            var result = await _controller.GetAllTrainings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTrainings = Assert.IsAssignableFrom<IEnumerable<Training>>(okResult.Value);
            Assert.Equal(2, ((List<Training>)returnedTrainings).Count);
        }

        [Fact]
        public async Task GetTrainingById_ReturnsOkResult_WithTraining()
        {
            // Arrange
            var training = new Training { EmployeeID = 101, TrainingName = "Safety Training" };
            ReflectionHelper.SetPropertyValue(training, "Id", 1);
            _mockTrainingService.Setup(service => service.GetTrainingByIdAsync(1))
                                .ReturnsAsync(training);

            // Act
            var result = await _controller.GetTrainingById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTraining = Assert.IsAssignableFrom<Training>(okResult.Value);
            Assert.Equal(1, returnedTraining.Id);
        }

        [Fact]
        public async Task CreateTraining_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var training = new Training { EmployeeID = 101, TrainingName = "Safety Training" };
            ReflectionHelper.SetPropertyValue(training, "Id", 1);
            _mockTrainingService.Setup(service => service.CreateTrainingAsync(training))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateTraining(training);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedTraining = Assert.IsAssignableFrom<Training>(createdAtActionResult.Value);
            Assert.Equal(training.Id, returnedTraining.Id);
            Assert.Equal(nameof(TrainingController.GetTrainingById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateTraining_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var training = new Training { EmployeeID = 101, TrainingName = "Safety Training" };
            ReflectionHelper.SetPropertyValue(training, "Id", 2);

            // Act
            var result = await _controller.UpdateTraining(1, training);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteTraining_ReturnsNoContentResult()
        {
            // Arrange
            _mockTrainingService.Setup(service => service.DeleteTrainingAsync(1))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTraining(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetTrainingsByEmployeeId_ReturnsOkResult_WithListOfTrainings()
        {
            // Arrange
            var trainings = new List<Training>
            {
                new Training { EmployeeID = 101, TrainingName = "Safety Training" },
                new Training { EmployeeID = 101, TrainingName = "Leadership Training" }
            };
            ReflectionHelper.SetPropertyValue(trainings[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(trainings[1], "Id", 2);
            _mockTrainingService.Setup(service => service.GetTrainingsByEmployeeIdAsync(101))
                                .ReturnsAsync(trainings);

            // Act
            var result = await _controller.GetTrainingsByEmployeeId(101);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTrainings = Assert.IsAssignableFrom<IEnumerable<Training>>(okResult.Value);
            Assert.Equal(2, ((List<Training>)returnedTrainings).Count);
        }
    }
}
