using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller, IDepartmentController
    {
        private ApiDataDbContext context;
        public DepartmentController(ApiDataDbContext context)
        {
                     this.context = context;
        }

        [Route("api/department")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            
            var data = await context.departments.ToListAsync();
            return await Task.FromResult<OkObjectResult>(Ok(data));
        }

        [HttpPost("api/department")]
        public async Task<IActionResult> PostAsync([FromBody] Department dept)
        {
            if (dept is null)
            {
                return await Task.FromResult<BadRequestResult>(BadRequest());
            }
            await context.departments.AddAsync(dept);
            await context.SaveChangesAsync();
            return await Task.FromResult<OkObjectResult>(Ok(dept));
        }

        [HttpGet("api/department/{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
           
            int _id = int.Parse(id);
            var data = await context.departments.FindAsync(_id);
            if (data == null) 
            { 
                return await Task.FromResult<NotFoundResult>(NotFound()); 
            }
            else
            {
                return await Task.FromResult<OkObjectResult>(Ok(data));
            }
        }
    }
}
