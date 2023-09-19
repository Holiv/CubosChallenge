using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinServicesContext _context;

        public AccountRepository(FinServicesContext context)
        {
            _context = context;
        }
        public async Task<bool> AccountExists(Guid accountId)
        {
            return await _context.Account.AnyAsync(account => account.Id == accountId);
        }

        public async Task<Account?> GetAccountAsync(Guid accountId)
        {
            return await _context.Account
                .Include(account => account.Cards)
                .Include(account => account.Transactions)
                .Where(account => account.Id == accountId)
                .FirstOrDefaultAsync();
        }

        public async Task AddCardsAccountAsync(Guid accountId, Card card)
        {
            var account = await GetAccountAsync(accountId);
            account?.Cards.Add(card);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasPhisicalCard(Guid accountId)
        {
            var account = await GetAccountAsync(accountId);
            return account.Cards.Any(card => card.Type == "fisico");
        }

        public async Task AddTransactionToAccountAsync(Guid accountId, Transaction transaction)
        {
            var account = await GetAccountAsync(accountId);
            account?.Transactions.Add(transaction);
            account.Balance += transaction.Value;
        }

        public async Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(Guid accountId)
        {
            return await _context.Transaction
                .Where(t => t.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<Transaction> GetAccountTransactionAsync(Guid accountId, Guid transactionId)
        {
            var account = await GetAccountAsync(accountId);
            return account.Transactions
                .Where(transaction => transaction.Id == transactionId)
                .FirstOrDefault();
        }

        public Task<bool> TransactionExists(Guid transactionId)
        {
            return _context.Account
                .AnyAsync(transaction => transaction.Id == transactionId);
        }
    }
}
