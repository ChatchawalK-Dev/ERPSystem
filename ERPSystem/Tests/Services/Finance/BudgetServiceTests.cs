using Xunit;
using Moq;
using ERPSystem.Models.Finance;
using ERPSystem.Data.Repository.Finance;
using ERPSystem.Services.Finance;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ERPSystem.Tests.Services.Finance
{
    public class BudgetServiceTests
    {
        private readonly Mock<IBudgetRepository> _mockRepository;
        private readonly IBudgetService _service;

        public BudgetServiceTests()
        {
            _mockRepository = new Mock<IBudgetRepository>();
            _service = new BudgetService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllBudgetsAsync_ReturnsAllBudgets()
        {
            // Arrange
            var budgets = new List<Budget>
            {
                new Budget { BudgetName = "Budget 1" },
                new Budget { BudgetName = "Budget 2" }
            };
            ReflectionHelper.SetPropertyValue(budgets[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(budgets[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(budgets);

            // Act
            var result = await _service.GetAllBudgetsAsync();

            // Assert
            Assert.NotNull(result);
            var budgetList = result.ToList();
            Assert.Equal(2, budgetList.Count);
            Assert.All(budgetList, budget => Assert.NotNull(budget.BudgetName));
        }

        [Fact]
        public async Task GetBudgetByIdAsync_ReturnsBudget()
        {
            // Arrange
            var budget = new Budget { BudgetName = "Budget 1" };
            ReflectionHelper.SetPropertyValue(budget, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(budget);

            // Act
            var result = await _service.GetBudgetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Budget 1", result?.BudgetName);
        }

        [Fact]
        public async Task CreateBudgetAsync_CreatesBudget()
        {
            // Arrange
            var budget = new Budget { BudgetName = "New Budget" };

            // Act
            await _service.CreateBudgetAsync(budget);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(budget), Times.Once);
        }

        [Fact]
        public async Task UpdateBudgetAsync_UpdatesBudget()
        {
            // Arrange
            var budget = new Budget { BudgetName = "Updated Budget" };

            // Act
            await _service.UpdateBudgetAsync(budget);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(budget), Times.Once);
        }

        [Fact]
        public async Task DeleteBudgetAsync_DeletesBudget()
        {
            // Arrange
            var budgetId = 1;

            // Act
            await _service.DeleteBudgetAsync(budgetId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(budgetId), Times.Once);
        }

        [Fact]
        public async Task GetBudgetsByDateRangeAsync_ReturnsBudgetsWithinDateRange()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);
            var budgets = new List<Budget>
    {
        new Budget { BudgetName = "Budget in Range", CreateAt = new DateTime(2024, 6, 15) },
        new Budget { BudgetName = "Another Budget in Range", CreateAt = new DateTime(2024, 7, 20) }
    };
            ReflectionHelper.SetPropertyValue(budgets[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(budgets[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetBudgetsByDateRangeAsync(startDate, endDate)).ReturnsAsync(budgets);

            // Act
            var result = await _service.GetBudgetsByDateRangeAsync(startDate, endDate);

            // Assert
            Assert.NotNull(result);
            var budgetList = result.ToList();
            Assert.Equal(2, budgetList.Count);
            Assert.All(budgetList, budget =>
            {
                Assert.NotNull(budget);
                Assert.InRange(budget.CreateAt, startDate, endDate);
            });
        }
    }
}
