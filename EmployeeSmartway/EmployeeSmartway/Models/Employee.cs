using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSmartway.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        /*public struct Passport
        {
            public string Type { get; set; }
            public string Number { get; set; }
            public Passport(string Type, string Number)
            {
                this.Type = Type;
                this.Number = Number;
            }
        }*/
    }
}
