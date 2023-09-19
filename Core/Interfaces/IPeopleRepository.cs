using Core.Entities;
using Core.SpecificationParams;
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
        Task<IEnumerable<Card>> GetPersonCardsAsync(Guid personId, SpecParamsEvaluator pagination); 
        Task AddPersonAccountAsync(Guid personId, Account account);
        Task SaveChangesAsync();
    }
}
