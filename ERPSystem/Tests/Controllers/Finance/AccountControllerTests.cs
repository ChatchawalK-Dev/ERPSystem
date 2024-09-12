using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using ERPSystem.Controllers.Finance;
using ERPSystem.Models.Finance;
using ERPSystem.Services.Finance;

namespace ERPSystem.Tests.Controllers.Finance
{
    public class AccountControllerTests
    {
        private readonly AccountController _controller;
        private readonly Mock<IAccountService> _mockAccountService;

        public AccountControllerTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new AccountController(_mockAccountService.Object);
        }

        [Fact]
        public async Task GetAllAccounts_ReturnsOkResult_WithAccounts()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account(),
                new Account()
            };
            ReflectionHelper.SetPropertyValue(accounts[0], "Id", 1);
            ReflectionHelper.SetPropertyValue(accounts[1], "Id", 2);

            _mockAccountService.Setup(service => service.GetAllAccountsAsync())
                               .ReturnsAsync(accounts);

            // Act
            var result = await _controller.GetAllAccounts();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Account>>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedAccounts = Assert.IsAssignableFrom<IEnumerable<Account>>(okResult.Value);

            Assert.Equal(accounts.Count, returnedAccounts.Count());
            foreach (var account in accounts)
            {
                var matchedAccount = returnedAccounts.FirstOrDefault(a => a.Id == account.Id);
                Assert.NotNull(matchedAccount);
            }
        }

        [Fact]
        public async Task GetAccountById_ReturnsOkResult_WithAccount()
        {
            // Arrange
            var account = new Account();
            ReflectionHelper.SetPropertyValue(account, "Id", 1);

            _mockAccountService.Setup(service => service.GetAccountByIdAsync(1))
                               .ReturnsAsync(account);

            // Act
            var result = await _controller.GetAccountById(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Account?>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedAccount = Assert.IsAssignableFrom<Account>(okResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnedAccount, "Id"));
        }

        [Fact]
        public async Task CreateAccount_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var account = new Account();
            ReflectionHelper.SetPropertyValue(account, "Id", 1);

            _mockAccountService.Setup(service => service.CreateAccountAsync(account))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateAccount(account);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult as CreatedAtActionResult);
            var returnValue = Assert.IsAssignableFrom<Account>(createdAtActionResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnValue, "Id"));
            Assert.Equal(nameof(AccountController.GetAccountById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateAccount_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var account = new Account();
            ReflectionHelper.SetPropertyValue(account, "Id", 1);

            _mockAccountService.Setup(service => service.UpdateAccountAsync(account))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateAccount(1, account);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAccount_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockAccountService.Setup(service => service.DeleteAccountAsync(1))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAccount(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAccountWithTransactions_ReturnsOkResult_WithAccount()
        {
            // Arrange
            var account = new Account();
            ReflectionHelper.SetPropertyValue(account, "Id", 1);

            _mockAccountService.Setup(service => service.GetAccountWithTransactionsAsync(1))
                               .ReturnsAsync(account);

            // Act
            var result = await _controller.GetAccountWithTransactions(1);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Account?>>(result);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var returnedAccount = Assert.IsAssignableFrom<Account>(okResult.Value);
            Assert.Equal(1, ReflectionHelper.GetPropertyValue(returnedAccount, "Id"));
        }
    }

    public static class ReflectionHelper
    {
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (property == null)
            {
                throw new System.ArgumentException($"Property '{propertyName}' not found.");
            }
            property.SetValue(obj, value);
        }

        public static object? GetPropertyValue(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (property == null)
            {
                throw new System.ArgumentException($"Property '{propertyName}' not found.");
            }
            return property.GetValue(obj);
        }
    }
}
