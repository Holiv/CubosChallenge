using Core.Entities;
using Core.Interfaces;
using Core.SpecificationParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly FinServicesContext _context;

        public PeopleRepository(FinServicesContext context)
        {
            _context = context;
        }
        public async Task AddPersonAsync(Person person)
        {
            await _context.Person.AddAsync(person);
        }

        public async Task<Person?> GetPersonAsync(Guid personId)
        {
            return await _context.Person
                .Include(p => p.Accounts)
                .Where(p => p.Id == personId).FirstOrDefaultAsync();
        }

        public async Task AddPersonAccountAsync(Guid personId, Account account)
        {
            var person = await GetPersonAsync(personId);
            person?.Accounts.Add(account);
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await _context.Person.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PersonExists(Guid personId)
        {
            return await _context.Person.AnyAsync(p => p.Id == personId);
        }

        public async Task<IEnumerable<Account>> GetPersonAccountsAsync(Guid personId)
        {
            return await _context.Account
                .Where(account => account.PersonId == personId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetPersonCardsAsync(Guid personId, PaginationEvaluator pagination)
        {
            return await _context.Card
                .Where(card => card.PersonId == personId)
                .Skip(pagination.Skip)
                .Take(pagination.Take)
                .ToListAsync();
        }
    }
}
