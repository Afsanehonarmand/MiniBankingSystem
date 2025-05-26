using MiniBankingSystem.Domain.Entities;

namespace MiniBankingSystem.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<Account?> GetByIdAsync(Guid accountId);
        Task<List<Account>> GetAllAsync();
        Task UpdateAsync(Account account);
    }

}
