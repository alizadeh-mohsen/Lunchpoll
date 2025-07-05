using LunchPoll.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LunchPoll.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return dbContext.Employees.Any()
                ? Ok( await dbContext.Employees.ToListAsync())
                : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var Employee = await dbContext.Employees.FirstOrDefaultAsync(r => r.Id == id);
            return Employee != null ? Ok(Employee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] string EmployeeName)
        {
            if (string.IsNullOrWhiteSpace(EmployeeName))
            {
                return BadRequest("Employee name cannot be empty.");
            }

            dbContext.Employees.Add(new Employee { Name = EmployeeName });
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployees), new { name = EmployeeName }, EmployeeName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] string EmployeeName)
        {
            if (string.IsNullOrWhiteSpace(EmployeeName))
            {
                return BadRequest("Employee name cannot be empty.");
            }
            var Employee = dbContext.Employees.FirstOrDefault(r => r.Id == id);
            if (Employee == null)
            {
                return NotFound();
            }
            Employee.Name = EmployeeName;
           await  dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteEmployee(string id)
        {
            var Employee = dbContext.Employees.FirstOrDefault(r => r.Id == id);
            if (Employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(Employee);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
