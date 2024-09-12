using ERPSystem.Controllers.Finance;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Models.Finance;
using ERPSystem.Services.Finance;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ERPSystem.Tests.Controllers.Finance
{
    public class BudgetControllerTests
    {
        private readonly Mock<IBudgetService> _mockBudgetService;
        private readonly BudgetController _controller;

        public BudgetControllerTests()
        {
            _mockBudgetService = new Mock<IBudgetService>();
            _controller = new BudgetController(_mockBudgetService.Object);
        }

        [Fact]
        public async Task GetAllBudgets_ReturnsOkResult_WithBudgets()
        {
            // Arrange
            var budgets = new List<Budget>
        {
            new Budget { BudgetName = "Budget1" },
            new Budget { BudgetName = "Budget2" }
        };
            _mockBudgetService.Setup(service => service.GetAllBudgetsAsync())
                              .ReturnsAsync(budgets);

            // Act
            var result = await _controller.GetAllBudgets();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Budget>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedBudgets = Assert.IsAssignableFrom<IEnumerable<Budget>>(okResult.Value);

            Assert.Equal(budgets.Count, returnedBudgets.Count());
            foreach (var budget in budgets)
            {
                var matchedBudget = returnedBudgets.FirstOrDefault(b => b.Id == budget.Id);
                Assert.NotNull(matchedBudget);
            }
        }

        [Fact]
        public async Task GetBudgetById_ReturnsOkResult_WhenBudgetExists()
        {
            // Arrange
            var budget = new Budget { BudgetName = "Budget1" };
            ReflectionHelper.SetPropertyValue(budget, "Id", 1);
            _mockBudgetService.Setup(service => service.GetBudgetByIdAsync(1))
                              .ReturnsAsync(budget);

            // Act
            var result = await _controller.GetBudgetById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Budget>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedBudget = Assert.IsType<Budget>(okResult.Value);

            Assert.Equal(budget.Id, returnedBudget.Id);
        }

        [Fact]
        public async Task GetBudgetById_ReturnsNotFound_WhenBudgetDoesNotExist()
        {
            // Arrange
            _mockBudgetService.Setup(service => service.GetBudgetByIdAsync(1))
                              .ReturnsAsync((Budget?)null);

            // Act
            var result = await _controller.GetBudgetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateBudget_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var budget = new Budget { BudgetName = "Budget1" };
            ReflectionHelper.SetPropertyValue(budget, "Id", 1);
            _mockBudgetService.Setup(service => service.CreateBudgetAsync(budget))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateBudget(budget);

            // Assert
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            var returnedBudget = Assert.IsAssignableFrom<Budget>(createdAtActionResult.Value);

            Assert.Equal(budget.Id, returnedBudget.Id);
            Assert.Equal(nameof(BudgetController.GetBudgetById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateBudget_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var budget = new Budget { BudgetName = "Budget1" };
            ReflectionHelper.SetPropertyValue(budget, "Id", 1);
            _mockBudgetService.Setup(service => service.UpdateBudgetAsync(budget))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateBudget(1, budget);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBudget_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var budget = new Budget { BudgetName = "Budget1" };
            ReflectionHelper.SetPropertyValue(budget, "Id", 2);
            // Act
            var result = await _controller.UpdateBudget(1, budget);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteBudget_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockBudgetService.Setup(service => service.DeleteBudgetAsync(1))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBudget(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBudgetsByDateRange_ReturnsOkResult_WithBudgets()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);
            var budgets = new List<Budget>
        {
            new Budget { BudgetName = "Budget1", StartDate = startDate.AddMonths(1) },
            new Budget { BudgetName = "Budget2", EndDate = endDate.AddMonths(-1) }
        };
            ReflectionHelper.SetPropertyValue(budgets[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(budgets[1], "Id", 2);
            _mockBudgetService.Setup(service => service.GetBudgetsByDateRangeAsync(startDate, endDate))
                              .ReturnsAsync(budgets);

            // Act
            var result = await _controller.GetBudgetsByDateRange(startDate, endDate);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Budget>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedBudgets = Assert.IsAssignableFrom<IEnumerable<Budget>>(okResult.Value);

            Assert.Equal(budgets.Count, returnedBudgets.Count());
            foreach (var budget in budgets)
            {
                var matchedBudget = returnedBudgets.FirstOrDefault(b => b.Id == budget.Id);
                Assert.NotNull(matchedBudget);
            }
        }
    }
}