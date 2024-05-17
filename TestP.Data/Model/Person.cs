using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestP.Data.Model.Enum;

namespace TestP.Data.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string?  FirstName { get; set; }
        public string?  Surname { get; set; }
        public int?  Age { get; set; }
        public Sex?  Sex { get; set; }
        public int?  Number { get; set; }
        public bool?  Active { get; set; }
       
    }
}
