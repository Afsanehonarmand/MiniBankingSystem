using MiniBankingSystem.Domain.Entities;

namespace MiniBankingSystem.Application.Interfaces
{
    public interface IBankingService
    {
        Task<Account> CreateAccountAsync(string ownerName, decimal initialBalance);
        Task DepositAsync(Guid accountId, decimal amount);
        Task WithdrawAsync(Guid accountId, decimal amount);
        Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount);
        Task<List<Transaction>> GetAccountStatementAsync(Guid accountId);
        Task<List<Account>> GetAllAccountsAsync();
    }

}
