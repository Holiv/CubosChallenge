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
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task SaveChangesAsync();
    }
}
