using ERPSystem.Models.Finance;
using ERPSystem.Data.Repository.Finance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services.Finance
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
        Task<Account?> GetAccountWithTransactionsAsync(int accountId);
    }

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _accountRepository.AddAsync(account);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAsync(account);
        }

        public async Task DeleteAccountAsync(int id)
        {
            await _accountRepository.DeleteAsync(id);
        }

        public async Task<Account?> GetAccountWithTransactionsAsync(int accountId)
        {
            return await _accountRepository.GetAccountWithTransactionsAsync(accountId);
        }
    }
}
