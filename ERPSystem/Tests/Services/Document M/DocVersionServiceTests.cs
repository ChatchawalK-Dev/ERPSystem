using ERPSystem.Data.Repository.DocumentM;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Models.Document_M;
using ERPSystem.Services.DocumentM;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Services.Document_M
{
    public class DocVersionServiceTests
    {
        private readonly DocVersionService _service;
        private readonly Mock<IDocVersionRepository> _mockRepository;

        public DocVersionServiceTests()
        {
            _mockRepository = new Mock<IDocVersionRepository>();
            _service = new DocVersionService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllDocVersionsAsync_ReturnsAllDocVersions()
        {
            // Arrange
            var docVersions = new List<DocVersion>
            {
                new DocVersion { VersionNumber = "1.0" },
                new DocVersion { VersionNumber = "2.0" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(docVersions);

            // Act
            var result = await _service.GetAllDocVersionsAsync();

            // Assert
            Assert.NotNull(result);
            var docVersionList = result.ToList();
            Assert.Equal(2, docVersionList.Count);
            Assert.All(docVersionList, dv => Assert.NotNull(dv.VersionNumber));
        }

        [Fact]
        public async Task GetDocVersionByIdAsync_ReturnsDocVersion()
        {
            // Arrange
            var docVersion = new DocVersion { VersionNumber = "1.0" };
            ReflectionHelper.SetPropertyValue(docVersion, "Id", 1);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(docVersion);

            // Act
            var result = await _service.GetDocVersionByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1.0", result?.VersionNumber);
        }

        [Fact]
        public async Task CreateDocVersionAsync_CreatesDocVersion()
        {
            // Arrange
            var docVersion = new DocVersion { VersionNumber = "1.0" };
            _mockRepository.Setup(repo => repo.AddAsync(docVersion)).Returns(Task.CompletedTask);

            // Act
            await _service.CreateDocVersionAsync(docVersion);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(docVersion), Times.Once);
        }

        [Fact]
        public async Task UpdateDocVersionAsync_UpdatesDocVersion()
        {
            // Arrange
            var docVersion = new DocVersion { VersionNumber = "1.0" };
            ReflectionHelper.SetPropertyValue(docVersion, "Id", 1);
            _mockRepository.Setup(repo => repo.UpdateAsync(docVersion)).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateDocVersionAsync(docVersion);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(docVersion), Times.Once);
        }

        [Fact]
        public async Task DeleteDocVersionAsync_DeletesDocVersion()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteDocVersionAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetDocVersionsWithDocumentsAsync_ReturnsDocVersionsWithDocuments()
        {
            // Arrange
            var docVersions = new List<DocVersion>
    {
        new DocVersion { VersionNumber = "1.0", Document = new Document { DocumentName = "Doc 1" } },
        new DocVersion { VersionNumber = "2.0", Document = new Document { DocumentName = "Doc 2" } }
    };

            ReflectionHelper.SetPropertyValue(docVersions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(docVersions[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetDocVersionsWithDocumentsAsync()).ReturnsAsync(docVersions);

            // Act
            var result = await _service.GetDocVersionsWithDocumentsAsync();

            // Assert
            Assert.NotNull(result);
            var docVersionList = result.ToList();
            Assert.Equal(2, docVersionList.Count);
            Assert.All(docVersionList, dv =>
            {
                Assert.NotNull(dv.Document);
                Assert.NotNull(dv.VersionNumber);
                Assert.NotEmpty(dv.VersionNumber);
            });
        }
    }
}
