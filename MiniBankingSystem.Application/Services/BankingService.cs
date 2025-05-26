using MiniBankingSystem.Application.Interfaces;
using MiniBankingSystem.Domain.Entities;
using MiniBankingSystem.Domain.Enums;
using MiniBankingSystem.Domain.Interfaces;

namespace MiniBankingSystem.Application.Services
{
    public class BankingService : IBankingService
    {
        private readonly IAccountRepository _accountRepository;

        public BankingService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccountAsync(string ownerName, decimal initialBalance)
        {
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative");

            var account = new Account(ownerName, initialBalance);
            await _accountRepository.AddAsync(account);
            return account;
        }

        public async Task DepositAsync(Guid accountId, decimal amount)
        {
            var account = await GetAccountOrThrowAsync(accountId);
            account.Deposit(amount);
            await _accountRepository.UpdateAsync(account);
        }

        public async Task WithdrawAsync(Guid accountId, decimal amount)
        {
            var account = await GetAccountOrThrowAsync(accountId);
            account.Withdraw(amount);
            await _accountRepository.UpdateAsync(account);
        }

        public async Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            if (fromAccountId == toAccountId)
                throw new ArgumentException("Cannot transfer to the same account");

            var from = await GetAccountOrThrowAsync(fromAccountId);
            var to = await GetAccountOrThrowAsync(toAccountId);

            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be positive");
            if (from.Balance < amount)
                throw new InvalidOperationException("Insufficient balance");

            from.Withdraw(amount);
            from.AddTransaction(TransactionType.Transfer, amount, to.Id);

            to.Deposit(amount);
            to.AddTransaction(TransactionType.Deposit, amount, from.Id);

            await _accountRepository.UpdateAsync(from);
            await _accountRepository.UpdateAsync(to);
        }

        public async Task<List<Transaction>> GetAccountStatementAsync(Guid accountId)
        {
            var account = await GetAccountOrThrowAsync(accountId);
            return account.Transactions.OrderByDescending(t => t.Date).ToList();
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        private async Task<Account> GetAccountOrThrowAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return account ?? throw new InvalidOperationException("Account not found");
        }
    }


}
