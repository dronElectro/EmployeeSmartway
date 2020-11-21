using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeSmartway.Models;

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

        







        //**************************************


        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.AddRange(employee);
            await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }/*, employee*/);
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeOnCOmpanyId(int companyId)
        {
            var temp = new List<Employee>();
            await foreach (Employee i in _context.Employees)
                if(i.CompanyId == companyId)
                {
                    temp.Add(i);
                }

            return temp;
        }


        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

            employeeInBase.Name = ((employee.Name != employeeInBase.Name) && (employee.Name != null)) ? employee.Name : employeeInBase.Name;
            employeeInBase.Surname = ((employee.Surname != employeeInBase.Surname) && (employee.Surname != null)) ? employee.Surname : employeeInBase.Surname;
            employeeInBase.Phone = ((employee.Phone != employeeInBase.Phone) && (employee.Phone != null)) ? employee.Phone : employeeInBase.Phone;
            employeeInBase.CompanyId = ((employee.CompanyId != employeeInBase.CompanyId) && (employee.CompanyId != 0)) ? employee.CompanyId : employeeInBase.CompanyId;

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
