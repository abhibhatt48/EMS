
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Models
{
    public class ApiDataDbContext : DbContext
    {
        public ApiDataDbContext()
        {
            
        }
        public ApiDataDbContext(DbContextOptions<ApiDataDbContext> options) : base(options)
        {
           
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }


    }
}
