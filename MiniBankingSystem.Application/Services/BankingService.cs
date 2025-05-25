using MiniBankingSystem.Domain.Entities;
using MiniBankingSystem.Domain.Enums;
using MiniBankingSystem.Domain.Interfaces;

namespace MiniBankingSystem.Application.Services
{
    public class BankingService
    {
        private readonly IAccountRepository _accountRepository;

        public BankingService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account CreateAccount(string ownerName, decimal initialBalance)
        {
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative");

            var account = new Account(ownerName, initialBalance);
            _accountRepository.Add(account);
            return account;
        }

        public void Deposit(Guid accountId, decimal amount)
        {
            var account = GetAccountOrThrow(accountId);
            account.Deposit(amount);
            _accountRepository.Update(account);
        }

        public void Withdraw(Guid accountId, decimal amount)
        {
            var account = GetAccountOrThrow(accountId);
            account.Withdraw(amount);
            _accountRepository.Update(account);
        }

        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            if (fromAccountId == toAccountId)
                throw new ArgumentException("Cannot transfer to the same account");

            var from = GetAccountOrThrow(fromAccountId);
            var to = GetAccountOrThrow(toAccountId);

            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be positive");
            if (from.Balance < amount)
                throw new InvalidOperationException("Insufficient balance");

            from.Withdraw(amount);
            from.AddTransaction(TransactionType.Transfer, amount, to.Id);

            to.Deposit(amount);
            to.AddTransaction(TransactionType.Deposit, amount, from.Id);

            _accountRepository.Update(from);
            _accountRepository.Update(to);
        }

        public List<Transaction> GetAccountStatement(Guid accountId)
        {
            var account = GetAccountOrThrow(accountId);
            return account.Transactions.OrderByDescending(t => t.Date).ToList();
        }

        private Account GetAccountOrThrow(Guid accountId)
        {
            var account = _accountRepository.GetById(accountId);
            if (account == null)
                throw new InvalidOperationException("Account not found");

            return account;
        }
    }

}
