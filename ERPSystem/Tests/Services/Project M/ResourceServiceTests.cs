using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using ERPSystem.Data.Repository.ProjectM;

namespace ERPSystem.Tests.Services.ProjectM
{
    public class ResourceServiceTests
    {
        private readonly Mock<IResourceRepository> _mockRepository;
        private readonly ResourceService _service;

        public ResourceServiceTests()
        {
            _mockRepository = new Mock<IResourceRepository>();
            _service = new ResourceService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllResourcesAsync_ReturnsAllResources()
        {
            // Arrange
            var resources = new List<Resource>
            {
                new Resource { /* Initialize properties */ },
                new Resource { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(resources[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(resources[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(resources);

            // Act
            var result = await _service.GetAllResourcesAsync();

            // Assert
            Assert.NotNull(result);
            var resourceList = result.ToList();
            Assert.Equal(2, resourceList.Count);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(resourceList[0], "Id"));
            Assert.Equal(2, ReflectionHelper.GetPropertyValue(resourceList[1], "Id"));
        }

        [Fact]
        public async Task GetResourceByIdAsync_ReturnsCorrectResource()
        {
            // Arrange
            var resource = new Resource { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(resource, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(resource);

            // Act
            var result = await _service.GetResourceByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreateResourceAsync_CallsRepositoryAdd()
        {
            // Arrange
            var resource = new Resource { /* Initialize properties */ };

            // Act
            await _service.CreateResourceAsync(resource);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(resource), Times.Once);
        }

        [Fact]
        public async Task UpdateResourceAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var resource = new Resource { /* Initialize properties */ };

            // Act
            await _service.UpdateResourceAsync(resource);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(resource), Times.Once);
        }

        [Fact]
        public async Task DeleteResourceAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteResourceAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
