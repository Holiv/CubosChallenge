using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPeopleRepository
    {
        Task AddPersonAsync(Person person);
        Task<bool> PersonExists(Guid personId);
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<IEnumerable<Account>> GetPersonAccountsAsync(Guid personId);
        Task<IEnumerable<Card>> GetPersonCardsAsync(Guid personId); 
        Task AddPersonAccountAsync(Guid personId, Account account);
        Task SaveChangesAsync();
    }
}
