using Xunit;
using Moq;
using ERPSystem.Models.Cr;
using ERPSystem.Repositories;
using ERPSystem.Services.Cr;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPSystem.Data.Repository.Cr;
using ERPSystem.Models.Project_M;
using ERPSystem.Models.Finance;

namespace ERPSystem.Tests.Services.Cr
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _service;
        private readonly Mock<ICustomerRepository> _mockRepository;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _service = new CustomerService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            ReflectionHelper.SetPropertyValue(customer, "Id", 1);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Customer?)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
        {
            new Customer { Name = "John Doe" },
            new Customer { Name = "Jane Doe" }
        };
            ReflectionHelper.SetPropertyValue(customers[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(customers[1], "Id", 2);
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var customerList = result.ToList();
            Assert.Equal(2, customerList.Count);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAdd()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            ReflectionHelper.SetPropertyValue(customer, "Id", 1);

            // Act
            await _service.AddAsync(customer);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            ReflectionHelper.SetPropertyValue(customer, "Id", 1);

            // Act
            await _service.UpdateAsync(customer);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
