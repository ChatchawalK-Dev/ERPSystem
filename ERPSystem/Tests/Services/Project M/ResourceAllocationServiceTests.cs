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
    public class ResourceAllocationServiceTests
    {
        private readonly Mock<IResourceAllocationRepository> _mockRepository;
        private readonly ResourceAllocationService _service;

        public ResourceAllocationServiceTests()
        {
            _mockRepository = new Mock<IResourceAllocationRepository>();
            _service = new ResourceAllocationService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAllocationsAsync_ReturnsAllAllocations()
        {
            // Arrange
            var allocations = new List<ResourceAllocation>
            {
                new ResourceAllocation { /* Initialize properties */ },
                new ResourceAllocation { /* Initialize properties */ }
            };
            ReflectionHelper.SetPropertyValue(allocations[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(allocations[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(allocations);

            // Act
            var result = await _service.GetAllAllocationsAsync();

            // Assert
            Assert.NotNull(result);
            var allocationList = result.ToList();
            Assert.Equal(2, allocationList.Count);
        }

        [Fact]
        public async Task GetAllocationByIdAsync_ReturnsCorrectAllocation()
        {
            // Arrange
            var allocation = new ResourceAllocation { /* Initialize properties */ };
            ReflectionHelper.SetPropertyValue(allocation, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(allocation);

            // Act
            var result = await _service.GetAllocationByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(result!, "Id"));
        }

        [Fact]
        public async Task CreateAllocationAsync_CallsRepositoryAdd()
        {
            // Arrange
            var allocation = new ResourceAllocation { /* Initialize properties */ };

            // Act
            await _service.CreateAllocationAsync(allocation);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(allocation), Times.Once);
        }

        [Fact]
        public async Task UpdateAllocationAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var allocation = new ResourceAllocation { /* Initialize properties */ };

            // Act
            await _service.UpdateAllocationAsync(allocation);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(allocation), Times.Once);
        }

        [Fact]
        public async Task DeleteAllocationAsync_CallsRepositoryDelete()
        {
            // Arrange
            int id = 1;

            // Act
            await _service.DeleteAllocationAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllocationsByProjectIdAsync_ReturnsAllocationsForProject()
        {
            // Arrange
            int projectId = 1;
            var allocations = new List<ResourceAllocation>
            {
                new ResourceAllocation { /* Initialize properties */ },
                new ResourceAllocation { /* Initialize properties */ }
            };
            _mockRepository.Setup(repo => repo.GetAllocationsByProjectIdAsync(projectId)).ReturnsAsync(allocations);

            // Act
            var result = await _service.GetAllocationsByProjectIdAsync(projectId);

            // Assert
            Assert.NotNull(result);
            var allocationList = result.ToList();
            Assert.Equal(2, allocationList.Count);
        }

        [Fact]
        public async Task GetAllocationsByResourceIdAsync_ReturnsAllocationsForResource()
        {
            // Arrange
            int resourceId = 1;
            var allocations = new List<ResourceAllocation>
            {
                new ResourceAllocation { /* Initialize properties */ },
                new ResourceAllocation { /* Initialize properties */ }
            };
            _mockRepository.Setup(repo => repo.GetAllocationsByResourceIdAsync(resourceId)).ReturnsAsync(allocations);

            // Act
            var result = await _service.GetAllocationsByResourceIdAsync(resourceId);

            // Assert
            Assert.NotNull(result);
            var allocationList = result.ToList();
            Assert.Equal(2, allocationList.Count);
        }
    }
}
