using ERPSystem.Controllers.HumanResources;
using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.HumanResources
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockEmployeeService.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Name = "John Doe" },
                new Employee { Name = "Jane Doe" }
            };
            ReflectionHelper.SetPropertyValue(employees[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(employees[1], "Id", 2);
            _mockEmployeeService.Setup(service => service.GetAllEmployeesAsync())
                                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Equal(2, ((List<Employee>)returnedEmployees).Count);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsOkResult_WithEmployee()
        {
            // Arrange
            var employee = new Employee { Name = "John Doe" };
            ReflectionHelper.SetPropertyValue(employee, "Id", 1);
            _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(1))
                                .ReturnsAsync(employee);

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsAssignableFrom<Employee>(okResult.Value);
            Assert.Equal(1, returnedEmployee.Id);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var employee = new Employee { Name = "John Doe" };
            ReflectionHelper.SetPropertyValue(employee, "Id", 1);
            _mockEmployeeService.Setup(service => service.CreateEmployeeAsync(employee))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateEmployee(employee);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedEmployee = Assert.IsAssignableFrom<Employee>(createdAtActionResult.Value);
            Assert.Equal(employee.Id, returnedEmployee.Id);
            Assert.Equal(nameof(EmployeeController.GetEmployeeById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var employee = new Employee { Name = "John Doe" };
            ReflectionHelper.SetPropertyValue(employee, "Id", 2);

            // Act
            var result = await _controller.UpdateEmployee(1, employee);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsNoContentResult()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.DeleteEmployeeAsync(1))
                                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteEmployee(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetEmployeesByPosition_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Name = "John Doe", Position = "Manager" },
                new Employee { Name = "Jane Doe", Position = "Manager" }
            };
            ReflectionHelper.SetPropertyValue(employees[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(employees[1], "Id", 2);
            _mockEmployeeService.Setup(service => service.GetEmployeesByPositionAsync("Manager"))
                                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetEmployeesByPosition("Manager");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Equal(2, ((List<Employee>)returnedEmployees).Count);
        }
    }
}
