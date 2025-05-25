using System.Collections.Concurrent;
using MiniBankingSystem.Domain.Entities;
using MiniBankingSystem.Domain.Interfaces;

namespace MiniBankingSystem.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly ConcurrentDictionary<Guid, Account> _accounts = new();

        public void Add(Account account)
        {
            _accounts.TryAdd(account.Id, account);
        }

        public Account? GetById(Guid id)
        {
            _accounts.TryGetValue(id, out var account);
            return account;
        }

        public void Update(Account account)
        {
            _accounts[account.Id] = account;
        }

        public List<Account> GetAll()
        {
            return _accounts.Values.ToList();
        }
    }
}
