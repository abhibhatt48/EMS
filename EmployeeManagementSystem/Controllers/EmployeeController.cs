using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{

    public class EmployeeController : Controller, IEmployeeController
    {
        private ApiDataDbContext context;

        public EmployeeController(ApiDataDbContext context)
        {
            this.context = context;
        }
        public static bool validateDepartment(Employee emp)
        {
            ApiDataDbContext context = new ApiDataDbContext();
            Department temp = context.departments.First(x => x.DepartmentId == emp.DepartmentId);
            return (temp == null) ? false : true;
        }

        [Route("api/employee")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            context = new ApiDataDbContext();
            var data = await context.employees.ToListAsync();
            return Ok(data);
        }


        [HttpGet("api/employee/{id}")]
        public async Task<IActionResult> GetEmpByIdAsync(string id)
        {
            context = new ApiDataDbContext();
            int _id = int.Parse(id);
            var employee = await context.employees.FindAsync(_id);
            
            if (employee == null) { return await Task.FromResult<NotFoundResult>(NotFound()); }
            else
            {
               return await Task.FromResult<OkObjectResult>(Ok(employee));
            }
        }


        //post employee valid ... 

        [HttpPost("api/employee")]

        public async Task<IActionResult> PostAsync([FromBody] Employee emp)
        {
            context = new ApiDataDbContext();
            var data = await context.employees.FirstOrDefaultAsync(x => x.EmployeeId == emp.EmployeeId);
            if (data == null)
            {

                if (validateDepartment(emp))
                {
                    context.employees.Add(emp);
                    context.SaveChanges();
                    return Created("api/employee", emp);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(data);
            }

        }
    }
}
