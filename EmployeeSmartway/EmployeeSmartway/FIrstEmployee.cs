using EmployeeSmartway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                        CompanyId = 1
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
