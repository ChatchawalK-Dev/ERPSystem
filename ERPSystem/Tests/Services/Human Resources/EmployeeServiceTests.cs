using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using ERPSystem.Data.Repository.HumanResources;

namespace ERPSystem.Tests.Services.HumanResources
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly IEmployeeService _service;

        public EmployeeServiceTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Name = "John Doe", Position = "Developer" },
                new Employee { Name = "Jane Smith", Position = "Manager" }
            };
            ReflectionHelper.SetPropertyValue(employees[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(employees[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _service.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            var employeeList = result.ToList();
            Assert.Equal(2, employeeList.Count);
            Assert.All(employeeList, employee =>
            {
                Assert.NotNull(employee);
                Assert.False(string.IsNullOrEmpty(employee.Name));
                Assert.False(string.IsNullOrEmpty(employee.Position));
            });
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployee()
        {
            // Arrange
            var employee = new Employee { Name = "John Doe", Position = "Developer" };
            ReflectionHelper.SetPropertyValue(employee, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _service.GetEmployeeByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result?.Name);
            Assert.Equal("Developer", result?.Position);
        }

        [Fact]
        public async Task CreateEmployeeAsync_CreatesEmployee()
        {
            // Arrange
            var employee = new Employee { Name = "John Doe", Position = "Developer" };

            // Act
            await _service.CreateEmployeeAsync(employee);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(employee), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_UpdatesEmployee()
        {
            // Arrange
            var employee = new Employee { Name = "John Doe", Position = "Developer" };

            // Act
            await _service.UpdateEmployeeAsync(employee);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(employee), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_DeletesEmployee()
        {
            // Act
            await _service.DeleteEmployeeAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetEmployeesByPositionAsync_ReturnsEmployeesByPosition()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Name = "John Doe", Position = "Developer" },
                new Employee { Name = "Jane Smith", Position = "Developer" }
            };
            ReflectionHelper.SetPropertyValue(employees[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(employees[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetEmployeesByPositionAsync("Developer")).ReturnsAsync(employees);

            // Act
            var result = await _service.GetEmployeesByPositionAsync("Developer");

            // Assert
            Assert.NotNull(result);
            var employeeList = result.ToList();
            Assert.Equal(2, employeeList.Count);
            Assert.All(employeeList, employee =>
            {
                Assert.NotNull(employee);
                Assert.Equal("Developer", employee.Position);
            });
        }
    }
}
