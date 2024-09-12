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
    public class AccessControlControllerTests
    {
        private readonly Mock<IAccessControlService> _mockAccessControlService;
        private readonly AccessControlController _controller;

        public AccessControlControllerTests()
        {
            _mockAccessControlService = new Mock<IAccessControlService>();
            _controller = new AccessControlController(_mockAccessControlService.Object);
        }

        [Fact]
        public async Task GetAllAccessControls_ReturnsOkResult_WithListOfAccessControls()
        {
            // Arrange
            var accessControls = new List<AccessControl> { new AccessControl(), new AccessControl() };
            _mockAccessControlService.Setup(service => service.GetAllAccessControlsAsync())
                                     .ReturnsAsync(accessControls);

            // Act
            var result = await _controller.GetAllAccessControls();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AccessControl>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAccessControlById_ReturnsOkResult_WithAccessControl()
        {
            // Arrange
            var accessControl = new AccessControl();
            ReflectionHelper.SetProperty(accessControl, "Id", 1);
            _mockAccessControlService.Setup(service => service.GetAccessControlByIdAsync(1))
                                     .ReturnsAsync(accessControl);

            // Act
            var result = await _controller.GetAccessControlById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AccessControl>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task CreateAccessControl_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var accessControl = new AccessControl();
            ReflectionHelper.SetProperty(accessControl, "Id", 1);
            _mockAccessControlService.Setup(service => service.CreateAccessControlAsync(accessControl))
                                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateAccessControl(accessControl);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<AccessControl>(createdAtActionResult.Value);
            Assert.Equal(accessControl.Id, returnValue.Id);
            Assert.Equal(nameof(AccessControlController.GetAccessControlById), createdAtActionResult.ActionName);
        }


        [Fact]
        public async Task UpdateAccessControl_ReturnsNoContentResult()
        {
            // Arrange
            var accessControl = new AccessControl();
            ReflectionHelper.SetProperty(accessControl, "Id", 1);
            _mockAccessControlService.Setup(service => service.UpdateAccessControlAsync(accessControl))
                                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateAccessControl(1, accessControl);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAccessControl_ReturnsNoContentResult()
        {
            // Arrange
            var accessControl = new AccessControl();
            ReflectionHelper.SetProperty(accessControl, "Id", 1);
            _mockAccessControlService.Setup(service => service.DeleteAccessControlAsync(1))
                                     .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAccessControl(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAccessControlsByUserId_ReturnsOkResult_WithListOfAccessControls()
        {
            // Arrange
            var accessControls = new List<AccessControl> { new AccessControl(), new AccessControl() };
            _mockAccessControlService.Setup(service => service.GetAccessControlsByUserIdAsync(1))
                                     .ReturnsAsync(accessControls);

            // Act
            var result = await _controller.GetAccessControlsByUserId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AccessControl>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAccessControlsByDocumentId_ReturnsOkResult_WithListOfAccessControls()
        {
            // Arrange
            var accessControls = new List<AccessControl> { new AccessControl(), new AccessControl() };
            _mockAccessControlService.Setup(service => service.GetAccessControlsByDocumentIdAsync(1))
                                     .ReturnsAsync(accessControls);

            // Act
            var result = await _controller.GetAccessControlsByDocumentId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AccessControl>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
