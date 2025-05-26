using MiniBankingSystem.Application.Interfaces;
using MiniBankingSystem.Application.Services;
using MiniBankingSystem.Domain.Enums;
using MiniBankingSystem.Domain.Interfaces;
using MiniBankingSystem.Infrastructure.Repositories;
using Xunit;

namespace MiniBankingSystem.Tests
{
    public class BankingServiceTests
    {
        private readonly IAccountRepository _repository;
        private readonly IBankingService _service;

        public BankingServiceTests()
        {
            _repository = new InMemoryAccountRepository();
            _service = new BankingService(_repository);
        }

        [Fact]
        public async Task CreateAccount_ShouldReturnAccountWithCorrectData()
        {
            var owner = "Ali";
            var initialBalance = 5000;

            var account = await _service.CreateAccountAsync(owner, initialBalance);

            Assert.Equal(owner, account.OwnerName);
            Assert.Equal(initialBalance, account.Balance);
        }

        [Fact]
        public async Task Deposit_ShouldIncreaseBalance()
        {
            var account = await _service.CreateAccountAsync("Test", 1000);
            await _service.DepositAsync(account.Id, 500);

            var updated = await _repository.GetByIdAsync(account.Id);
            Assert.Equal(1500, updated!.Balance);
        }

        [Fact]
        public async Task Withdraw_ShouldThrowException_WhenInsufficientFunds()
        {
            var account = await _service.CreateAccountAsync("Test", 1000);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _service.WithdrawAsync(account.Id, 2000);
            });
        }

        [Fact]
        public async Task Transfer_ShouldMoveFundsBetweenAccounts()
        {
            var from = await _service.CreateAccountAsync("From", 1000);
            var to = await _service.CreateAccountAsync("To", 500);

            await _service.TransferAsync(from.Id, to.Id, 300);

            var fromUpdated = await _repository.GetByIdAsync(from.Id);
            var toUpdated = await _repository.GetByIdAsync(to.Id);

            Assert.Equal(700, fromUpdated!.Balance);
            Assert.Equal(800, toUpdated!.Balance);
        }


        [Fact]
        public async Task GetAccountStatement_ShouldReturnTransactions()
        {
            var account = await _service.CreateAccountAsync("HistoryTest", 1000);
            await _service.DepositAsync(account.Id, 200);
            await _service.WithdrawAsync(account.Id, 100);

            var transactions = await _service.GetAccountStatementAsync(account.Id);

            Assert.Equal(2, transactions.Count);
            Assert.Contains(transactions, t => t.Type == TransactionType.Deposit && t.Amount == 200);
            Assert.Contains(transactions, t => t.Type == TransactionType.Withdrawal && t.Amount == 100);
        }

    }
}
