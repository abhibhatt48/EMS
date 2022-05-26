using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementSystem.UnitTests.Controllers
{
    public class EmployeeControllerTest
    {
        private  EmployeeController _controller;
        private  ApiDataDbContext _context;
        
        
        public ApiDataDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApiDataDbContext>();
            options.UseInMemoryDatabase("DatabaseName:employees");
            var dbase = new ApiDataDbContext(options.Options);
            _context.departments.Add(new Department { DepartmentId = 1, DepartmentName = "Developer" });
            _context.departments.Add(new Department { DepartmentId = 2, DepartmentName = "Tester" });
            _context.employees.Add(new Employee { EmployeeId = 1, Name = "Saumil", Surname = "Patel", DepartmentId = 1 });
            _context.employees.Add(new Employee { EmployeeId = 2, Name = " Darshan", Surname = "Patel", DepartmentId = 1 });
            _context.employees.Add(new Employee { EmployeeId = 3, Name = "Ashiyana", Surname = "Nai avadto spelling", DepartmentId = 2 });
            return _context;
        }


        [Fact]
        public async Task GetAsync_Execute_ReturnView()
        {
            _context = GetContext();
          
            _controller = new EmployeeController(_context);
            _context.employees.Add(new Employee { });
            var result = _controller.GetAsync();
            Assert.NotNull(result);
           _context.Database.EnsureDeleted();
        }

       
       


       
    }
}
