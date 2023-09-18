using Core.Entities;
using Core.Interfaces;
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

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await _context.Person.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
