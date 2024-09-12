using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ERPSystem.Models.Document_M;
using ERPSystem.Services.DocumentM;
using ERPSystem.Data.Repository.DocumentM;
using ERPSystem.Models.Data_Analytics;

namespace ERPSystem.Tests.Services.Document_M
{
    public class AccessControlServiceTests
    {
        private readonly Mock<IAccessControlRepository> _mockRepository;
        private readonly AccessControlService _service;

        public AccessControlServiceTests()
        {
            _mockRepository = new Mock<IAccessControlRepository>();
            _service = new AccessControlService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAccessControlsAsync_ReturnsAllAccessControls()
        {
            // Arrange
            var accessControls = new List<AccessControl>
            {
                new AccessControl { UserID = 1, AccessLevel = "Test" },
                new AccessControl { UserID = 2, AccessLevel = "Test" }
            };

            ReflectionHelper.SetPropertyValue(accessControls[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(accessControls[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(accessControls);

            // Act
            var result = await _service.GetAllAccessControlsAsync();

            // Assert
            Assert.NotNull(result);
            var accessControlList = result.ToList();
            Assert.Equal(2, accessControlList.Count);
            Assert.Contains(accessControlList, ac => ac.Id == 1 && ac.UserID == 1 && ac.AccessLevel == "Test");
            Assert.Contains(accessControlList, ac => ac.Id == 2 && ac.UserID == 2 && ac.AccessLevel == "Test");
        }

        [Fact]
        public async Task GetAccessControlByIdAsync_ReturnsAccessControl()
        {
            // Arrange
            var accessControl = new AccessControl { UserID = 1, AccessLevel = "Test" };
            ReflectionHelper.SetPropertyValue(accessControl, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(accessControl);

            // Act
            var result = await _service.GetAccessControlByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.UserID);
            Assert.Equal("Test", result.AccessLevel);
        }

        [Fact]
        public async Task CreateAccessControlAsync_CallsRepositoryAdd()
        {
            // Arrange
            var accessControl = new AccessControl { UserID = 1, AccessLevel = "Test" };
            ReflectionHelper.SetPropertyValue(accessControl, "Id", 1);

            // Act
            await _service.CreateAccessControlAsync(accessControl);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<AccessControl>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAccessControlAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var accessControl = new AccessControl { UserID = 1, AccessLevel = "Test" };
            ReflectionHelper.SetPropertyValue(accessControl, "Id", 1);

            // Act
            await _service.UpdateAccessControlAsync(accessControl);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<AccessControl>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAccessControlAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteAccessControlAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAccessControlsByUserIdAsync_ReturnsAccessControls()
        {
            // Arrange
            var accessControls = new List<AccessControl>
    {
        new AccessControl { UserID = 1, AccessLevel = "Test" },
        new AccessControl { UserID = 2, AccessLevel = "Test" }
    };

            // Set the Id properties if needed
            ReflectionHelper.SetPropertyValue(accessControls[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(accessControls[1], "Id", 2);

            // Mock repository setup
            _mockRepository.Setup(repo => repo.GetAccessControlsByUserIdAsync(1)).ReturnsAsync(
                accessControls.Where(ac => ac.UserID == 1).ToList()
            );

            // Act
            var result = await _service.GetAccessControlsByUserIdAsync(1);

            // Assert
            Assert.NotNull(result);
            var accessControlList = result.ToList();
            Assert.Single(accessControlList);
            Assert.Equal(1, accessControlList.First().UserID);  
        }

        [Fact]
        public async Task GetAccessControlsByDocumentIdAsync_ReturnsAccessControls()
        {
            // Arrange
            var accessControls = new List<AccessControl>
            {
                new AccessControl { UserID = 1, AccessLevel = "Test" },
                new AccessControl { UserID = 2, AccessLevel = "Test" }
            };

            ReflectionHelper.SetPropertyValue(accessControls[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(accessControls[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAccessControlsByDocumentIdAsync(1)).ReturnsAsync(accessControls);

            // Act
            var result = await _service.GetAccessControlsByDocumentIdAsync(1);

            // Assert
            Assert.NotNull(result);
            var accessControlList = result.ToList();
            Assert.Equal(2, accessControlList.Count);
            Assert.All(accessControlList, ac => Assert.Equal("Test", ac.AccessLevel));
        }
    }
}
