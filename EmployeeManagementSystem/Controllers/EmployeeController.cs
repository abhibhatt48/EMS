using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{

    public class EmployeeController : Controller, IEmployeeController
    {
        private ApiDataDbContext context;

        public EmployeeController(ApiDataDbContext context)
        {
            this.context = context;
        }
        public bool validateDepartment(Employee emp)
        {
            Department temp = context.departments.FirstOrDefault(x => x.DepartmentId == emp.DepartmentId);
            return (temp == null) ? false : true;
        }

        [Route("api/employee")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await context.employees.ToListAsync();
            return await Task.FromResult<OkObjectResult>(Ok(data));
        }


        [HttpGet("api/employee/{id}")]
        public async Task<IActionResult> GetEmpByIdAsync(string id)
        {
           
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
                if (validateDepartment(emp))
                {
                   await context.employees.AddAsync(emp);
                    await context.SaveChangesAsync();
                    return await Task.FromResult<CreatedResult>(Created("api/employee", emp));
                }
                else
                {
                    return await Task.FromResult<BadRequestResult>(BadRequest());
                }
        }
    }
}
