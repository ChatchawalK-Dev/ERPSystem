using ERPSystem.Controllers.Cr;
using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Cr
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer { Name = "John Doe" };
            ReflectionHelper.SetProperty(customer, "Id", customerId); // Set Id using reflection
            _mockCustomerService.Setup(service => service.GetByIdAsync(customerId))
                                .ReturnsAsync(customer);

            // Act
            var result = await _controller.GetById(customerId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, returnValue.Id);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Name = "John Doe" },
                new Customer { Name = "Jane Doe" }
            };
            _mockCustomerService.Setup(service => service.GetAllAsync())
                                .ReturnsAsync(customers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Customer>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.Equal(2, ((List<Customer>)returnValue).Count);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            ReflectionHelper.SetProperty(customer, "Id", 1); // Set Id using reflection
            _mockCustomerService.Setup(service => service.AddAsync(customer))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(customer);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Customer>(createdAtActionResult.Value);
            Assert.Equal(customer.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContentResult()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            ReflectionHelper.SetProperty(customer, "Id", 1); // Set Id using reflection
            _mockCustomerService.Setup(service => service.UpdateAsync(customer))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(customer.Id, customer);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var customerId = 1;
            _mockCustomerService.Setup(service => service.DeleteAsync(customerId))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(customerId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
