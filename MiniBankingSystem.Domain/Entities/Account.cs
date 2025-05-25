using MiniBankingSystem.Domain.Enums;

namespace MiniBankingSystem.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public decimal Balance { get; private set; } = 0;
    public List<Transaction> Transactions { get; private set; } = new();

    public Account(string ownerName, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("Owner name is required.");

        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.");

        Id = Guid.NewGuid();
        OwnerName = ownerName;
        Balance = initialBalance;
        AddTransaction(TransactionType.Deposit, initialBalance);
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than zero.");

        if (amount <= 0) throw new ArgumentException("Amount must be positive");
        Balance += amount;
        AddTransaction(TransactionType.Deposit, amount);
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be greater than zero.");
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient balance.");

        if (amount <= 0) throw new ArgumentException("Amount must be positive");
        if (amount > Balance) throw new InvalidOperationException("Insufficient funds");
        Balance -= amount;
        AddTransaction(TransactionType.Withdrawal, amount);
    }

    public void AddTransaction(TransactionType type, decimal amount, Guid? targetAccountId = null)
    {
        Transactions.Add(new Transaction
        {
            Id = Guid.NewGuid(),
            Type = type,
            Amount = amount,
            Date = DateTime.UtcNow,
            AccountId = Id,
            TargetAccountId = targetAccountId
        });
    }
}

