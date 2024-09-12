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
    public class RecruitmentServiceTests
    {
        private readonly Mock<IRecruitmentRepository> _mockRepository;
        private readonly IRecruitmentService _service;

        public RecruitmentServiceTests()
        {
            _mockRepository = new Mock<IRecruitmentRepository>();
            _service = new RecruitmentService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllRecruitmentsAsync_ReturnsAllRecruitments()
        {
            // Arrange
            var recruitments = new List<Recruitment>
            {
                new Recruitment { Position = "Software Engineer", Status = "Open" },
                new Recruitment { Position = "Product Manager", Status = "Closed" }
            };
            ReflectionHelper.SetPropertyValue(recruitments[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(recruitments[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(recruitments);

            // Act
            var result = await _service.GetAllRecruitmentsAsync();

            // Assert
            Assert.NotNull(result);
            var recruitmentList = result.ToList();
            Assert.Equal(2, recruitmentList.Count);
            Assert.All(recruitmentList, recruitment =>
            {
                Assert.NotNull(recruitment);
                Assert.False(string.IsNullOrEmpty(recruitment.Position));
                Assert.False(string.IsNullOrEmpty(recruitment.Status));
            });
        }

        [Fact]
        public async Task GetRecruitmentByIdAsync_ReturnsRecruitment()
        {
            // Arrange
            var recruitment = new Recruitment { Position = "Software Engineer", Status = "Open" };
            ReflectionHelper.SetPropertyValue(recruitment, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(recruitment);

            // Act
            var result = await _service.GetRecruitmentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Software Engineer", result?.Position);
            Assert.Equal("Open", result?.Status);
        }

        [Fact]
        public async Task CreateRecruitmentAsync_CreatesRecruitment()
        {
            // Arrange
            var recruitment = new Recruitment { Position = "Software Engineer", Status = "Open" };

            // Act
            await _service.CreateRecruitmentAsync(recruitment);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(recruitment), Times.Once);
        }

        [Fact]
        public async Task UpdateRecruitmentAsync_UpdatesRecruitment()
        {
            // Arrange
            var recruitment = new Recruitment { Position = "Software Engineer", Status = "Open" };

            // Act
            await _service.UpdateRecruitmentAsync(recruitment);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(recruitment), Times.Once);
        }

        [Fact]
        public async Task DeleteRecruitmentAsync_DeletesRecruitment()
        {
            // Act
            await _service.DeleteRecruitmentAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetRecruitmentsByStatusAsync_ReturnsRecruitmentsByStatus()
        {
            // Arrange
            var recruitments = new List<Recruitment>
            {
                new Recruitment { Position = "Software Engineer", Status = "Open" },
                new Recruitment { Position = "Product Manager", Status = "Open" }
            };
            ReflectionHelper.SetPropertyValue(recruitments[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(recruitments[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetRecruitmentsByStatusAsync("Open")).ReturnsAsync(recruitments);

            // Act
            var result = await _service.GetRecruitmentsByStatusAsync("Open");

            // Assert
            Assert.NotNull(result);
            var recruitmentList = result.ToList();
            Assert.Equal(2, recruitmentList.Count);
            Assert.All(recruitmentList, recruitment =>
            {
                Assert.NotNull(recruitment);
                Assert.Equal("Open", recruitment.Status);
            });
        }
    }
}
