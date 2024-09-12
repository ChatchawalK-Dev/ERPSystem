using Moq;
using Xunit;
using ERPSystem.Models.Document_M;
using ERPSystem.Data.Repository.DocumentM;
using ERPSystem.Services.DocumentM;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPSystem.Models.Data_Analytics;

namespace ERPSystem.Tests.Services.Document_M
{
    public class DocumentServiceTests
    {
        private readonly Mock<IDocumentRepository> _mockRepository;
        private readonly DocumentService _service;

        public DocumentServiceTests()
        {
            _mockRepository = new Mock<IDocumentRepository>();
            _service = new DocumentService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllDocumentsAsync_ReturnsDocuments()
        {
            // Arrange
            var documents = new List<Document>
        {
            new Document { DocumentName = "Document 1" },
            new Document { DocumentName = "Document 2" }
        };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(documents);

            // Act
            var result = await _service.GetAllDocumentsAsync();

            // Assert
            Assert.NotNull(result);
            var documentList = result.ToList();
            Assert.Equal(2, documentList.Count);
            Assert.Equal("Document 1", documentList[0].DocumentName);
            Assert.Equal("Document 2", documentList[1].DocumentName);
        }

        [Fact]
        public async Task GetDocumentByIdAsync_ReturnsDocument()
        {
            // Arrange
            var document = new Document { DocumentName = "Document 1" };
            ReflectionHelper.SetPropertyValue(document, "Id", 1);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(document);

            // Act
            var result = await _service.GetDocumentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Document 1", result?.DocumentName);
        }

        [Fact]
        public async Task CreateDocumentAsync_CreatesDocument()
        {
            // Arrange
            var document = new Document { DocumentName = "Document 1" };
            ReflectionHelper.SetPropertyValue(document, "Id", 1);
            // Act
            await _service.CreateDocumentAsync(document);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(document), Times.Once);
        }

        [Fact]
        public async Task UpdateDocumentAsync_UpdatesDocument()
        {
            // Arrange
            var document = new Document { DocumentName = "Updated Document" };
            ReflectionHelper.SetPropertyValue(document, "Id", 1);

            // Act
            await _service.UpdateDocumentAsync(document);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(document), Times.Once);
        }

        [Fact]
        public async Task DeleteDocumentAsync_DeletesDocument()
        {
            // Act
            await _service.DeleteDocumentAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetDocumentsWithAccessControlsAsync_ReturnsDocumentsWithAccessControls()
        {
            // Arrange
            var documents = new List<Document>
    {
        new Document { DocumentName = "Document 1", AccessControls = new List<AccessControl> { new AccessControl { UserID = 1 } } },
        new Document { DocumentName = "Document 2", AccessControls = new List<AccessControl> { new AccessControl { UserID = 2 } } }
    };

            ReflectionHelper.SetPropertyValue(documents[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(documents[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetDocumentsWithAccessControlsAsync()).ReturnsAsync(documents);

            // Act
            var result = await _service.GetDocumentsWithAccessControlsAsync();

            // Assert
            Assert.NotNull(result);
            var documentList = result.ToList();
            Assert.Equal(2, documentList.Count);

            // Assert that AccessControls is not null before asserting NotEmpty
            foreach (var document in documentList)
            {
                Assert.NotNull(document.AccessControls);
                Assert.NotEmpty(document.AccessControls);
            }
        }

        [Fact]
        public async Task GetDocumentsWithVersionsAsync_ReturnsDocumentsWithVersions()
        {
            // Arrange
            var documents = new List<Document>
        {
            new Document { DocumentName = "Document 1", DocVersions = new List<DocVersion> { new DocVersion { VersionNumber = "1.0" } } },
            new Document { DocumentName = "Document 2", DocVersions = new List<DocVersion> { new DocVersion { VersionNumber = "2.0" } } }
        };
            ReflectionHelper.SetPropertyValue(documents[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(documents[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetDocumentsWithVersionsAsync()).ReturnsAsync(documents);

            // Act
            var result = await _service.GetDocumentsWithVersionsAsync();

            // Assert
            Assert.NotNull(result);
            var documentList = result.ToList();
            Assert.Equal(2, documentList.Count);
            foreach (var document in documentList)
            {
                Assert.NotNull(document.DocVersions);
                Assert.NotEmpty(document.DocVersions);
            }
        }
    }
}
