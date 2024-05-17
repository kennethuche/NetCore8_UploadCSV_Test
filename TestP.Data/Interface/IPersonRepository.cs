using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestP.Data.Model;

namespace TestP.Data.Interface
{
    public interface IPersonRepository
    {
        Task<int> AddPersonAsync(Person person);
        Task<int> AddPersonsAsync(IEnumerable<Person> people);
    }

}
