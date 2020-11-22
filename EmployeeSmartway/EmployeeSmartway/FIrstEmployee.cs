using EmployeeSmartway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EmployeeSmartway.Models.Employee;

namespace EmployeeSmartway
{
    public static class FIrstEmployee
    {
        public static void Initialize(EmployeeContext context)
        {
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                    new Employee
                    {
                        Name = "Andrey",
                        Surname = "Brevnov",
                        Phone = "+79374449205",
                        CompanyId = 1,
                        
                        //Passport{ "555","555555"}
                        //passport { "5555","555555"}
                        //(Type="555", number="555555")=> 
                        //Passport(Type, number)

                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
