﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeSmartway.Models;
using static EmployeeSmartway.Models.Employee;
//final
namespace EmployeeSmartway.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller //Base
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }*/

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.AddRange(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), employee.Id);
        }


        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // GET: api/Employees
        [HttpGet("{companyid}")]
        //[HttpGet("companyid{companyid}")] - если есть необходимость использовать обычный get по id сотрудника
        //и нужно различать с get по company id
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeOnCOmpanyId(int companyId)
        {
            var temp = new List<Employee>();
            await foreach (Employee i in _context.Employees)
                if(i.CompanyId == companyId)
                {
                    temp.Add(i);
                    //temp.AddRange(_context.Employees.Where(_context.Employees.fi=>_context.Employees.))
                }
            return temp;
        }


        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
                _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Employee>> PatchEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            Employee employeeInBase = await _context.Employees.SingleAsync(x => x.Id == id);
            PassportEmp temp = new PassportEmp();

            //Проверка на обнуление какого либо поля (в запрос значение поля, которое необходимо обнулить, необходимо отправить '-1')
            if (employee.Name == "-1") employeeInBase.Name = null;
            else if (employee.Name != employeeInBase.Name && employee.Name != null) employeeInBase.Name = employee.Name;
            if (employee.Surname == "-1") employeeInBase.Surname = null;
            else if (employee.Surname != employeeInBase.Surname && employee.Surname != null) employeeInBase.Surname = employee.Surname;
            if (employee.Phone == "-1") employeeInBase.Phone = null;
            else if (employee.Phone != employeeInBase.Phone && employee.Phone != null) employeeInBase.Phone = employee.Phone;
            if (employee.CompanyId == -1) employeeInBase.CompanyId = 0; 
            else if (employee.CompanyId != employeeInBase.CompanyId && employee.CompanyId != 0) employeeInBase.CompanyId = employee.CompanyId;
            if (employee.Passport.Number == "-1") { temp = employeeInBase.Passport; temp.Number = null; employeeInBase.Passport = temp; }
            else if (employee.Passport.Number != employeeInBase.Passport.Number && employee.Passport.Number != null)
            {temp = employeeInBase.Passport; temp.Number = employee.Passport.Number; employeeInBase.Passport = temp;}
            if (employee.Passport.Type == "-1") { temp = employeeInBase.Passport; temp.Type = null; employeeInBase.Passport = temp; }
            else if (employee.Passport.Type != employeeInBase.Passport.Type && employee.Passport.Type != null)
            { temp = employeeInBase.Passport; temp.Type = employee.Passport.Type; employeeInBase.Passport = temp; }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return employeeInBase;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
