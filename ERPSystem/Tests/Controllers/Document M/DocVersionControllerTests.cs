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
    public class DocVersionControllerTests
    {
        private readonly Mock<IDocVersionService> _mockDocVersionService;
        private readonly DocVersionController _controller;

        public DocVersionControllerTests()
        {
            _mockDocVersionService = new Mock<IDocVersionService>();
            _controller = new DocVersionController(_mockDocVersionService.Object);
        }

        [Fact]
        public async Task GetAllDocVersions_ReturnsOkResult_WithListOfDocVersions()
        {
            // Arrange
            var docVersions = new List<DocVersion>
            {
                new DocVersion(), // Placeholder object
                new DocVersion()  // Placeholder object
            };

            // Use reflection to set Id if necessary
            ReflectionHelper.SetPropertyValue(docVersions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(docVersions[1], "Id", 2);

            _mockDocVersionService.Setup(service => service.GetAllDocVersionsAsync())
                                  .ReturnsAsync(docVersions);

            // Act
            var result = await _controller.GetAllDocVersions();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<DocVersion>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedDocVersions = Assert.IsAssignableFrom<IEnumerable<DocVersion>>(okResult.Value);
            Assert.Equal(docVersions, returnedDocVersions);
        }

        [Fact]
        public async Task GetDocVersionById_ReturnsNotFound_WhenDocVersionNotFound()
        {
            // Arrange
            _mockDocVersionService.Setup(service => service.GetDocVersionByIdAsync(1))
                                  .ReturnsAsync((DocVersion?)null);

            // Act
            var result = await _controller.GetDocVersionById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<DocVersion?>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateDocVersion_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var docVersion = new DocVersion();
            ReflectionHelper.SetPropertyValue(docVersion, "Id", 1);

            _mockDocVersionService.Setup(service => service.CreateDocVersionAsync(docVersion))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateDocVersion(docVersion);

            // Assert
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            var returnValue = Assert.IsAssignableFrom<DocVersion>(createdAtActionResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnValue, "Id"));
            Assert.Equal(nameof(DocVersionController.GetDocVersionById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateDocVersion_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var docVersion = new DocVersion();
            ReflectionHelper.SetPropertyValue(docVersion, "Id", 1);

            _mockDocVersionService.Setup(service => service.UpdateDocVersionAsync(docVersion))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateDocVersion(1, docVersion);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteDocVersion_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockDocVersionService.Setup(service => service.DeleteDocVersionAsync(1))
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteDocVersion(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetDocVersionsWithDocuments_ReturnsOkResult_WithListOfDocVersions()
        {
            // Arrange
            var docVersions = new List<DocVersion>
            {
                new DocVersion(), // Placeholder object
                new DocVersion()  // Placeholder object
            };

            // Use reflection to set Id if necessary
            ReflectionHelper.SetPropertyValue(docVersions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(docVersions[1], "Id", 2);

            _mockDocVersionService.Setup(service => service.GetDocVersionsWithDocumentsAsync())
                                  .ReturnsAsync(docVersions);

            // Act
            var result = await _controller.GetDocVersionsWithDocuments();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<DocVersion>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedDocVersions = Assert.IsAssignableFrom<IEnumerable<DocVersion>>(okResult.Value);
            Assert.Equal(docVersions, returnedDocVersions);
        }
    }
}
