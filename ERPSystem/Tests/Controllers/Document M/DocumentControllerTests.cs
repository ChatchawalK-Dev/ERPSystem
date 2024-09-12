using ERPSystem.Controllers.DocumentM;
using ERPSystem.Models.Document_M;
using ERPSystem.Services.DocumentM;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.DocumentM
{
    public class DocumentControllerTests
    {
        private readonly DocumentController _controller;
        private readonly Mock<IDocumentService> _mockDocumentService;

        public DocumentControllerTests()
        {
            _mockDocumentService = new Mock<IDocumentService>();
            _controller = new DocumentController(_mockDocumentService.Object);
        }

        [Fact]
        public async Task GetAllDocuments_ReturnsOkResult_WithListOfDocuments()
        {
            // Arrange
            var documents = new List<Document> { new Document(), new Document() };
            _mockDocumentService.Setup(service => service.GetAllDocumentsAsync())
                                .ReturnsAsync(documents);

            // Act
            var result = await _controller.GetAllDocuments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Document>>(okResult.Value);
            Assert.Equal(documents.Count, returnValue.Count);
        }

        [Fact]
        public async Task GetDocumentById_ReturnsOkResult_WithDocument()
        {
            // Arrange
            var document = new Document();
            ReflectionHelper.SetProperty(document, "Id", 1);
            _mockDocumentService.Setup(service => service.GetDocumentByIdAsync(1))
                                .ReturnsAsync(document);

            // Act
            var result = await _controller.GetDocumentById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Document>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetDocumentById_ReturnsNotFound_WhenDocumentNotFound()
        {
            // Arrange
            _mockDocumentService.Setup(service => service.GetDocumentByIdAsync(1))
                                .ReturnsAsync((Document?)null);

            // Act
            var result = await _controller.GetDocumentById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Document?>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateDocument_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var document = new Document();
            ReflectionHelper.SetProperty(document, "Id", 1);
            _mockDocumentService.Setup(service => service.CreateDocumentAsync(document))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateDocument(document);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Document>(createdAtActionResult.Value);
            Assert.Equal(document.Id, returnValue.Id);
            Assert.Equal(nameof(DocumentController.GetDocumentById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateDocument_ReturnsNoContent_WhenDocumentIsUpdated()
        {
            // Arrange
            var document = new Document();
            ReflectionHelper.SetProperty(document, "Id", 1);
            _mockDocumentService.Setup(service => service.UpdateDocumentAsync(document))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateDocument(1, document);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateDocument_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var document = new Document();
            ReflectionHelper.SetProperty(document, "Id", 1);

            // Act
            var result = await _controller.UpdateDocument(2, document);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteDocument_ReturnsNoContent_WhenDocumentIsDeleted()
        {
            // Arrange
            var document = new Document();
            ReflectionHelper.SetProperty(document, "Id", 1);
            _mockDocumentService.Setup(service => service.DeleteDocumentAsync(1))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteDocument(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetDocumentsWithAccessControls_ReturnsOkResult_WithListOfDocuments()
        {
            // Arrange
            var documents = new List<Document> { new Document(), new Document() };
            _mockDocumentService.Setup(service => service.GetDocumentsWithAccessControlsAsync())
                                .ReturnsAsync(documents);

            // Act
            var result = await _controller.GetDocumentsWithAccessControls();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Document>>(okResult.Value);
            Assert.Equal(documents.Count, returnValue.Count);
        }

        [Fact]
        public async Task GetDocumentsWithVersions_ReturnsOkResult_WithListOfDocuments()
        {
            // Arrange
            var documents = new List<Document> { new Document(), new Document() };
            _mockDocumentService.Setup(service => service.GetDocumentsWithVersionsAsync())
                                .ReturnsAsync(documents);

            // Act
            var result = await _controller.GetDocumentsWithVersions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Document>>(okResult.Value);
            Assert.Equal(documents.Count, returnValue.Count);
        }
    }
}
