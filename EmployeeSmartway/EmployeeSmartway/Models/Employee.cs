using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Type { get; set; }
        public string Number { get; set; }
        public struct PassportEmp
        {
            public string Type;
            public string Number;
        }

        [NotMapped]
        public PassportEmp Passport
        {
            get {
                PassportEmp pass = new PassportEmp();
                pass.Number = this.Number;
                pass.Type = this.Type;

                return pass;
            }
            set {
                this.Number = value.Number;
                this.Type = value.Type;
            }
        }
    }
}
