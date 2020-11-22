using Nancy.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
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
        [JsonIgnore] public string Type { get; private set; }
        [JsonIgnore] public string Number { get; private set; }
        public struct PassportEmp
        {
            public string Type { get; set; }
            public string Number { get; set; }
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
