using System.Collections.Concurrent;
using MiniBankingSystem.Domain.Entities;
using MiniBankingSystem.Domain.Interfaces;

namespace MiniBankingSystem.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly ConcurrentDictionary<Guid, Account> _accounts = new();

        public Task AddAsync(Account account)
        {
            _accounts.TryAdd(account.Id, account);
            return Task.CompletedTask;
        }

        public Task<Account?> GetByIdAsync(Guid accountId)
        {
            _accounts.TryGetValue(accountId, out var account);
            return Task.FromResult(account);
        }

        public Task<List<Account>> GetAllAsync()
        {
            return Task.FromResult(_accounts.Values.ToList());
        }

        public Task UpdateAsync(Account account)
        {
            // In-memory update is implicit because the reference is already in the dictionary
            _accounts[account.Id] = account;
            return Task.CompletedTask;
        }
    }

}