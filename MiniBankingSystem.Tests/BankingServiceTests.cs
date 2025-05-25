using MiniBankingSystem.Application.Services;
using MiniBankingSystem.Domain.Entities;
using MiniBankingSystem.Domain.Interfaces;
using Xunit;
using System;
using MiniBankingSystem.Infrastructure.Repositories;

namespace MiniBankingSystem.Tests
{
    public class BankingServiceTests
    {
        private readonly IAccountRepository _repository;
        private readonly BankingService _service;

        public BankingServiceTests()
        {
            _repository = new InMemoryAccountRepository();
            _service = new BankingService(_repository);
        }

        [Fact]
        public void CreateAccount_ShouldReturnAccountWithCorrectData()
        {
            var owner = "Ali";
            var initialBalance = 5000;

            var account = _service.CreateAccount(owner, initialBalance);

            Assert.Equal(owner, account.OwnerName);
            Assert.Equal(initialBalance, account.Balance);
        }

        [Fact]
        public void Deposit_ShouldIncreaseBalance()
        {
            var account = _service.CreateAccount("Test", 1000);
            _service.Deposit(account.Id, 500);

            var updated = _repository.GetById(account.Id);
            Assert.Equal(1500, updated!.Balance);
        }

        [Fact]
        public void Withdraw_ShouldThrowException_WhenInsufficientFunds()
        {
            var account = _service.CreateAccount("Test", 1000);

            Assert.Throws<InvalidOperationException>(() =>
            {
                _service.Withdraw(account.Id, 2000);
            });
        }

        [Fact]
        public void Transfer_ShouldMoveFundsBetweenAccounts()
        {
            var from = _service.CreateAccount("From", 1000);
            var to = _service.CreateAccount("To", 500);

            _service.Transfer(from.Id, to.Id, 300);

            var fromUpdated = _repository.GetById(from.Id);
            var toUpdated = _repository.GetById(to.Id);

            Assert.Equal(700, fromUpdated!.Balance);
            Assert.Equal(800, toUpdated!.Balance);
        }
    }
}
