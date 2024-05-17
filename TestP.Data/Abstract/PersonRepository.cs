using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestP.Data.Context;
using TestP.Data.Interface;
using TestP.Data.Model;

namespace TestP.Data.Abstract
{
    public class PersonRepository : IPersonRepository
    {
        protected readonly ApplicationDBContext _dbContext;
        public PersonRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<int> AddPersonAsync(Person person)
        {
            _dbContext.Persons.Add(person);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddPersonsAsync(IEnumerable<Person> people)
        {
            _dbContext.Persons.AddRange(people);
            return await _dbContext.SaveChangesAsync();
        }


    }
}
