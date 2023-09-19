using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> AccountExists(Guid accountId);
        Task AddCardsAccountAsync(Guid accountId, Card card);
        Task AddTransactionToAccountAsync(Guid accountId, Transaction transaction);
        Task<Account?> GetAccountAsync(Guid accountId);
        Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(Guid accountId);
        Task<bool> HasPhisicalCard(Guid accountId);
        Task SaveChangesAsync();
    }
}
