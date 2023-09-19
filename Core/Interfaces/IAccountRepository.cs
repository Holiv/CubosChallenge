using Core.Entities;
using Core.SpecificationParams;
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
        Task<bool> TransactionExists(Guid transactionId);
        Task AddCardsAccountAsync(Guid accountId, Card card);
        Task AddTransactionToAccountAsync(Guid accountId, Transaction transaction);
        Task<Account?> GetAccountAsync(Guid accountId);
        Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(Guid accountId, PaginationEvaluator paginationEvaluator);
        Task<Transaction> GetAccountTransactionAsync(Guid accountId, Guid transactionId);
        Task<bool> HasPhisicalCard(Guid accountId);
        Task SaveChangesAsync();
    }
}
