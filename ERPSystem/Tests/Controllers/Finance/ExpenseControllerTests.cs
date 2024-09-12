using ERPSystem.Controllers.Finance;
using ERPSystem.Models.Finance;
using ERPSystem.Services.Finance;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Finance
{
    public class ExpenseControllerTests
    {
        private readonly Mock<IExpenseService> _mockExpenseService;
        private readonly ExpenseController _controller;

        public ExpenseControllerTests()
        {
            _mockExpenseService = new Mock<IExpenseService>();
            _controller = new ExpenseController(_mockExpenseService.Object);
        }

        [Fact]
        public async Task GetAllExpenses_ReturnsOkResult_WithListOfExpenses()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense { Description = "Expense1" },
                new Expense { Description = "Expense2" }
            };
            ReflectionHelper.SetPropertyValue(expenses[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(expenses[1], "Id", 2);
            _mockExpenseService.Setup(service => service.GetAllExpensesAsync())
                               .ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetAllExpenses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedExpenses = Assert.IsAssignableFrom<IEnumerable<Expense>>(okResult.Value);
            Assert.Equal(2, ((List<Expense>)returnedExpenses).Count);
        }

        [Fact]
        public async Task GetExpenseById_ReturnsOkResult_WithExpense()
        {
            // Arrange
            var expense = new Expense { Description = "Expense1" };
            ReflectionHelper.SetPropertyValue(expense, "Id", 1);
            _mockExpenseService.Setup(service => service.GetExpenseByIdAsync(1))
                               .ReturnsAsync(expense);

            // Act
            var result = await _controller.GetExpenseById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedExpense = Assert.IsAssignableFrom<Expense>(okResult.Value);
            Assert.Equal(1, returnedExpense.Id);
        }

        [Fact]
        public async Task CreateExpense_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var expense = new Expense { Description = "Expense1" };
            ReflectionHelper.SetPropertyValue(expense, "Id", 1);
            _mockExpenseService.Setup(service => service.CreateExpenseAsync(expense))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateExpense(expense);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedExpense = Assert.IsAssignableFrom<Expense>(createdAtActionResult.Value);
            Assert.Equal(expense.Id, returnedExpense.Id);
            Assert.Equal(nameof(ExpenseController.GetExpenseById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateExpense_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var expense = new Expense { Description = "Expense1" };
            ReflectionHelper.SetPropertyValue(expense, "Id", 2);

            // Act
            var result = await _controller.UpdateExpense(1, expense);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteExpense_ReturnsNoContentResult()
        {
            // Arrange
            _mockExpenseService.Setup(service => service.DeleteExpenseAsync(1))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteExpense(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetExpensesByBudgetId_ReturnsOkResult_WithListOfExpenses()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense { Description = "Expense1" },
                new Expense { Description = "Expense2" }
            };
            ReflectionHelper.SetPropertyValue(expenses[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(expenses[1], "Id", 2);
            _mockExpenseService.Setup(service => service.GetExpensesByBudgetIdAsync(1))
                               .ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetExpensesByBudgetId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedExpenses = Assert.IsAssignableFrom<IEnumerable<Expense>>(okResult.Value);
            Assert.Equal(2, ((List<Expense>)returnedExpenses).Count);
        }
    }
}
