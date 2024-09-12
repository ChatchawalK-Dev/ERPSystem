using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ERPSystem.Models.Finance;
using ERPSystem.Services.Finance;
using ERPSystem.Data.Repository.Finance;

namespace ERPSystem.Tests.Services.Finance
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _mockRepository;
        private readonly ITransactionService _service;

        public TransactionServiceTests()
        {
            _mockRepository = new Mock<ITransactionRepository>();
            _service = new TransactionService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ReturnsAllTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Amount = 100, Description = "Transaction 1" },
                new Transaction { Amount = 200, Description = "Transaction 2" }
            };
            ReflectionHelper.SetPropertyValue(transactions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(transactions[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transactions);

            // Act
            var result = await _service.GetAllTransactionsAsync();

            // Assert
            Assert.NotNull(result);
            var transactionList = result.ToList();
            Assert.Equal(2, transactionList.Count);
            Assert.All(transactionList, transaction =>
            {
                Assert.NotNull(transaction);
                Assert.True(transaction.Amount > 0);
                Assert.False(string.IsNullOrEmpty(transaction.Description));
            });
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ReturnsTransaction()
        {
            // Arrange
            var transaction = new Transaction { Amount = 100, Description = "Transaction 1" };
            ReflectionHelper.SetPropertyValue(transaction, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(transaction);

            // Act
            var result = await _service.GetTransactionByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result?.Amount);
            Assert.Equal("Transaction 1", result?.Description);
        }

        [Fact]
        public async Task CreateTransactionAsync_CreatesTransaction()
        {
            // Arrange
            var transaction = new Transaction { Amount = 100, Description = "Transaction 1" };

            // Act
            await _service.CreateTransactionAsync(transaction);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(transaction), Times.Once);
        }

        [Fact]
        public async Task UpdateTransactionAsync_UpdatesTransaction()
        {
            // Arrange
            var transaction = new Transaction { Amount = 100, Description = "Transaction 1" };

            // Act
            await _service.UpdateTransactionAsync(transaction);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(transaction), Times.Once);
        }

        [Fact]
        public async Task DeleteTransactionAsync_DeletesTransaction()
        {
            // Act
            await _service.DeleteTransactionAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetTransactionsByAccountIdAsync_ReturnsTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Amount = 100, Description = "Transaction 1" },
                new Transaction { Amount = 200, Description = "Transaction 2" }
            };
            ReflectionHelper.SetPropertyValue(transactions[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(transactions[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetTransactionsByAccountIdAsync(1)).ReturnsAsync(transactions);

            // Act
            var result = await _service.GetTransactionsByAccountIdAsync(1);

            // Assert
            Assert.NotNull(result);
            var transactionList = result.ToList();
            Assert.Equal(2, transactionList.Count);
            Assert.All(transactionList, transaction =>
            {
                Assert.NotNull(transaction);
                Assert.True(transaction.Amount > 0);
                Assert.False(string.IsNullOrEmpty(transaction.Description));
            });
        }
    }
}
