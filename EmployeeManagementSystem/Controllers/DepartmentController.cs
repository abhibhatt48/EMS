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
            context = new ApiDataDbContext();
            var data = await context.departments.ToListAsync();
            // var data = await context.departments;
            return Ok(data);
        }

        [HttpPost("api/department")]
        public async Task<IActionResult> PostAsync([FromBody] Department dept)
        {
            if (dept is null)
            {
                throw new ArgumentNullException(nameof(dept));
            }

            context = new ApiDataDbContext();
            context.departments.Add(dept);
            //await context.departments.Add(dept);
            await context.SaveChangesAsync();
            return Ok(dept);
        }

        [HttpGet("api/department/{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            context = new ApiDataDbContext();
            int _id = int.Parse(id);
            var data = await context.departments.FirstOrDefaultAsync(d => d.DepartmentId == _id);
            if (data == null) { return NotFound(); }
            else
            {
                return Ok(data);
            }
        }
    }
}
