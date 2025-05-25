using MiniBankingSystem.Domain.Entities;

namespace MiniBankingSystem.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Account? GetById(Guid id);
        void Add(Account account);
        void Update(Account account);
        List<Account> GetAll();
    }
}
