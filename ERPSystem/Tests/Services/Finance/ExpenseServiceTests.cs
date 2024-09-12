using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPSystem.Models.Finance;
using ERPSystem.Services.Finance;
using ERPSystem.Data.Repository.Finance;

namespace ERPSystem.Tests.Services.Finance
{
    public class ExpenseServiceTests
    {
        private readonly Mock<IExpenseRepository> _mockRepository;
        private readonly IExpenseService _service;

        public ExpenseServiceTests()
        {
            _mockRepository = new Mock<IExpenseRepository>();
            _service = new ExpenseService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllExpensesAsync_ReturnsAllExpenses()
        {
            // Arrange
            var expenses = new List<Expense>
    {
        new Expense { Description = "Expense 1" },
        new Expense { Description = "Expense 2" }
    };
            ReflectionHelper.SetPropertyValue(expenses[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(expenses[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expenses);

            // Act
            var result = await _service.GetAllExpensesAsync();

            // Assert
            Assert.NotNull(result);
            var expenseList = result.ToList();
            Assert.Equal(2, expenseList.Count);
            Assert.All(expenseList, expense =>
            {
                Assert.NotNull(expense);
                Assert.False(string.IsNullOrEmpty(expense.Description));
            });
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ReturnsExpense()
        {
            // Arrange
            var expense = new Expense { Description = "Expense 1" };
            ReflectionHelper.SetPropertyValue(expense, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expense);

            // Act
            var result = await _service.GetExpenseByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Expense 1", result?.Description);
        }

        [Fact]
        public async Task CreateExpenseAsync_CreatesExpense()
        {
            // Arrange
            var expense = new Expense { Description = "New Expense" };

            // Act
            await _service.CreateExpenseAsync(expense);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(expense), Times.Once);
        }

        [Fact]
        public async Task UpdateExpenseAsync_UpdatesExpense()
        {
            // Arrange
            var expense = new Expense { Description = "Updated Expense" };

            // Act
            await _service.UpdateExpenseAsync(expense);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(expense), Times.Once);
        }

        [Fact]
        public async Task DeleteExpenseAsync_DeletesExpense()
        {
            // Act
            await _service.DeleteExpenseAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetExpensesByBudgetIdAsync_ReturnsExpensesForBudget()
        {
            // Arrange
            var budgetId = 1;
            var expenses = new List<Expense>
        {
            new Expense { Description = "Expense 1" },
            new Expense { Description = "Expense 2" }
        };
            ReflectionHelper.SetPropertyValue(expenses[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(expenses[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetExpensesByBudgetIdAsync(budgetId)).ReturnsAsync(expenses);

            // Act
            var result = await _service.GetExpensesByBudgetIdAsync(budgetId);

            // Assert
            Assert.NotNull(result);
            var expenseList = result.ToList();
            Assert.Equal(2, expenseList.Count);
            Assert.All(expenseList, expense =>
            {
                Assert.NotNull(expense);
                Assert.False(string.IsNullOrEmpty(expense.Description));
            });
        }
    }
}
