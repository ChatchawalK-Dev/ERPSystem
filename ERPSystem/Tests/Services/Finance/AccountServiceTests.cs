using Xunit;
using Moq;
using ERPSystem.Models.Finance;
using ERPSystem.Data.Repository.Finance;
using ERPSystem.Services.Finance;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Tests.Services.Finance
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _mockRepository;
        private readonly IAccountService _service;

        public AccountServiceTests()
        {
            _mockRepository = new Mock<IAccountRepository>();
            _service = new AccountService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAccountsAsync_ReturnsAllAccounts()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account { AccountName = "Account 1" },
                new Account { AccountName = "Account 2" }
            };
            ReflectionHelper.SetPropertyValue(accounts[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(accounts[1], "Id", 2);

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(accounts);

            // Act
            var result = await _service.GetAllAccountsAsync();

            // Assert
            Assert.NotNull(result);
            var accountList = result.ToList();
            Assert.Equal(2, accountList.Count);
            Assert.All(accountList, account => Assert.NotNull(account.AccountName));
        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnsAccount()
        {
            // Arrange
            var account = new Account { AccountName = "Account 1" };
            ReflectionHelper.SetPropertyValue(account, "Id", 1);

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(account);

            // Act
            var result = await _service.GetAccountByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Account 1", result?.AccountName);
        }

        [Fact]
        public async Task CreateAccountAsync_CreatesAccount()
        {
            // Arrange
            var account = new Account { AccountName = "New Account" };

            // Act
            await _service.CreateAccountAsync(account);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(account), Times.Once);
        }

        [Fact]
        public async Task UpdateAccountAsync_UpdatesAccount()
        {
            // Arrange
            var account = new Account { AccountName = "Updated Account" };

            // Act
            await _service.UpdateAccountAsync(account);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(account), Times.Once);
        }

        [Fact]
        public async Task DeleteAccountAsync_DeletesAccount()
        {
            // Arrange
            var accountId = 1;

            // Act
            await _service.DeleteAccountAsync(accountId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(accountId), Times.Once);
        }

        [Fact]
        public async Task GetAccountWithTransactionsAsync_ReturnsAccountWithTransactions()
        {
            // Arrange
            var account = new Account
            {
                AccountName = "Account with Transactions",
                Transactions = new List<Transaction> { new Transaction { Amount = 100 } }
            };
            ReflectionHelper.SetPropertyValue(account, "Id", 1);

            _mockRepository.Setup(repo => repo.GetAccountWithTransactionsAsync(1)).ReturnsAsync(account);

            // Act
            var result = await _service.GetAccountWithTransactionsAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Transactions);
            Assert.NotEmpty(result.Transactions);
        }
    }
}
