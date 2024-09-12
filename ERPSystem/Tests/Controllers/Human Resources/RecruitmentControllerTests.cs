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
    public class RecruitmentControllerTests
    {
        private readonly Mock<IRecruitmentService> _mockRecruitmentService;
        private readonly RecruitmentController _controller;

        public RecruitmentControllerTests()
        {
            _mockRecruitmentService = new Mock<IRecruitmentService>();
            _controller = new RecruitmentController(_mockRecruitmentService.Object);
        }

        [Fact]
        public async Task GetAllRecruitments_ReturnsOkResult_WithListOfRecruitments()
        {
            // Arrange
            var recruitments = new List<Recruitment>
            {
                new Recruitment { Position = "Developer" },
                new Recruitment { Position = "Designer" }
            };
            ReflectionHelper.SetPropertyValue(recruitments[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(recruitments[1], "Id", 2);
            _mockRecruitmentService.Setup(service => service.GetAllRecruitmentsAsync())
                                   .ReturnsAsync(recruitments);

            // Act
            var result = await _controller.GetAllRecruitments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRecruitments = Assert.IsAssignableFrom<IEnumerable<Recruitment>>(okResult.Value);
            Assert.Equal(2, ((List<Recruitment>)returnedRecruitments).Count);
        }

        [Fact]
        public async Task GetRecruitmentById_ReturnsOkResult_WithRecruitment()
        {
            // Arrange
            var recruitment = new Recruitment { Position = "Developer" };
            ReflectionHelper.SetPropertyValue(recruitment, "Id", 1);
            _mockRecruitmentService.Setup(service => service.GetRecruitmentByIdAsync(1))
                                   .ReturnsAsync(recruitment);

            // Act
            var result = await _controller.GetRecruitmentById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRecruitment = Assert.IsAssignableFrom<Recruitment>(okResult.Value);
            Assert.Equal(1, returnedRecruitment.Id);
        }

        [Fact]
        public async Task CreateRecruitment_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var recruitment = new Recruitment { Position = "Developer" };
            ReflectionHelper.SetPropertyValue(recruitment, "Id", 1);
            _mockRecruitmentService.Setup(service => service.CreateRecruitmentAsync(recruitment))
                                   .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateRecruitment(recruitment);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedRecruitment = Assert.IsAssignableFrom<Recruitment>(createdAtActionResult.Value);
            Assert.Equal(recruitment.Id, returnedRecruitment.Id);
            Assert.Equal(nameof(RecruitmentController.GetRecruitmentById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateRecruitment_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var recruitment = new Recruitment { Position = "Developer" };
            ReflectionHelper.SetPropertyValue(recruitment, "Id", 2);

            // Act
            var result = await _controller.UpdateRecruitment(1, recruitment);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteRecruitment_ReturnsNoContentResult()
        {
            // Arrange
            _mockRecruitmentService.Setup(service => service.DeleteRecruitmentAsync(1))
                                   .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteRecruitment(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetRecruitmentsByStatus_ReturnsOkResult_WithListOfRecruitments()
        {
            // Arrange
            var recruitments = new List<Recruitment>
            {
                new Recruitment { Position = "Developer", Status = "Open" },
                new Recruitment { Position = "Designer", Status = "Open" }
            };
            ReflectionHelper.SetPropertyValue(recruitments[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(recruitments[1], "Id", 2);
            _mockRecruitmentService.Setup(service => service.GetRecruitmentsByStatusAsync("Open"))
                                   .ReturnsAsync(recruitments);

            // Act
            var result = await _controller.GetRecruitmentsByStatus("Open");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRecruitments = Assert.IsAssignableFrom<IEnumerable<Recruitment>>(okResult.Value);
            Assert.Equal(2, ((List<Recruitment>)returnedRecruitments).Count);
        }
    }
}
