using MiniBankingSystem.Domain.Entities;
using MiniBankingSystem.Domain.Enums;
using Xunit;

namespace MiniBankingSystem.Tests.Domain
{
    public class AccountTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAccountCorrectly()
        {
            var account = new Account("Alice", 1000);

            Assert.Equal("Alice", account.OwnerName);
            Assert.Equal(1000, account.Balance);
            Assert.Single(account.Transactions);
            Assert.Equal(TransactionType.Deposit, account.Transactions[0].Type);
            Assert.Equal(1000, account.Transactions[0].Amount);
        }

        [Fact]
        public void Constructor_ShouldThrowException_ForEmptyOwner()
        {
            Assert.Throws<ArgumentException>(() => new Account("", 1000));
        }

        [Fact]
        public void Constructor_ShouldThrowException_ForNegativeBalance()
        {
            Assert.Throws<ArgumentException>(() => new Account("Bob", -100));
        }

        [Fact]
        public void Deposit_ShouldIncreaseBalanceAndAddTransaction()
        {
            var account = new Account("Test", 500);
            account.Deposit(200);

            Assert.Equal(700, account.Balance);
            Assert.Equal(2, account.Transactions.Count);
            Assert.Contains(account.Transactions, t => t.Type == TransactionType.Deposit && t.Amount == 200);
        }

        [Fact]
        public void Deposit_ShouldThrowException_WhenAmountIsZeroOrNegative()
        {
            var account = new Account("Test", 500);

            Assert.Throws<ArgumentException>(() => account.Deposit(0));
            Assert.Throws<ArgumentException>(() => account.Deposit(-100));
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalanceAndAddTransaction()
        {
            var account = new Account("Test", 1000);
            account.Withdraw(300);

            Assert.Equal(700, account.Balance);
            Assert.Equal(2, account.Transactions.Count);
            Assert.Contains(account.Transactions, t => t.Type == TransactionType.Withdrawal && t.Amount == 300);
        }

        [Fact]
        public void Withdraw_ShouldThrowException_WhenAmountIsZeroOrNegative()
        {
            var account = new Account("Test", 500);

            Assert.Throws<ArgumentException>(() => account.Withdraw(0));
            Assert.Throws<ArgumentException>(() => account.Withdraw(-50));
        }

        [Fact]
        public void Withdraw_ShouldThrowException_WhenInsufficientBalance()
        {
            var account = new Account("Test", 100);

            Assert.Throws<InvalidOperationException>(() => account.Withdraw(150));
        }
    }
}
