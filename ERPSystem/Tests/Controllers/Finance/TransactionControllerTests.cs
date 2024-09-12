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
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> _mockTransactionService;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _mockTransactionService = new Mock<ITransactionService>();
            _controller = new TransactionController(_mockTransactionService.Object);
        }

        [Fact]
        public async Task GetAllTransactions_ReturnsOkResult_WithListOfTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Description = "Transaction1" },
                new Transaction { Description = "Transaction2" }
            };
            ReflectionHelper.SetPropertyValue(transactions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(transactions[1], "Id", 2);
            _mockTransactionService.Setup(service => service.GetAllTransactionsAsync())
                                   .ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetAllTransactions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTransactions = Assert.IsAssignableFrom<IEnumerable<Transaction>>(okResult.Value);
            Assert.Equal(2, ((List<Transaction>)returnedTransactions).Count);
        }

        [Fact]
        public async Task GetTransactionById_ReturnsOkResult_WithTransaction()
        {
            // Arrange
            var transaction = new Transaction { Description = "Transaction1" };
            ReflectionHelper.SetPropertyValue(transaction, "Id", 1);
            _mockTransactionService.Setup(service => service.GetTransactionByIdAsync(1))
                                   .ReturnsAsync(transaction);

            // Act
            var result = await _controller.GetTransactionById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTransaction = Assert.IsAssignableFrom<Transaction>(okResult.Value);
            Assert.Equal(1, returnedTransaction.Id);
        }

        [Fact]
        public async Task CreateTransaction_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var transaction = new Transaction { Description = "Transaction1" };
            ReflectionHelper.SetPropertyValue(transaction, "Id", 1);
            _mockTransactionService.Setup(service => service.CreateTransactionAsync(transaction))
                                   .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateTransaction(transaction);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedTransaction = Assert.IsAssignableFrom<Transaction>(createdAtActionResult.Value);
            Assert.Equal(transaction.Id, returnedTransaction.Id);
            Assert.Equal(nameof(TransactionController.GetTransactionById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateTransaction_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var transaction = new Transaction { Description = "Transaction1" };
            ReflectionHelper.SetPropertyValue(transaction, "Id", 2);

            // Act
            var result = await _controller.UpdateTransaction(1, transaction);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteTransaction_ReturnsNoContentResult()
        {
            // Arrange
            _mockTransactionService.Setup(service => service.DeleteTransactionAsync(1))
                                   .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTransaction(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetTransactionsByAccountId_ReturnsOkResult_WithListOfTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Description = "Transaction1" },
                new Transaction { Description = "Transaction2" }
            };
            ReflectionHelper.SetPropertyValue(transactions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(transactions[1], "Id", 2);
            _mockTransactionService.Setup(service => service.GetTransactionsByAccountIdAsync(1))
                                   .ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetTransactionsByAccountId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTransactions = Assert.IsAssignableFrom<IEnumerable<Transaction>>(okResult.Value);
            Assert.Equal(2, ((List<Transaction>)returnedTransactions).Count);
        }
    }
}
