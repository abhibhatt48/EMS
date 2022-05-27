using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EmployeeManagementSystemTest
{
    public class Tests
    {
        private ApiDataDbContext _context;

        private ApiDataDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApiDataDbContext>();
            options.UseInMemoryDatabase("DatabaseName : EMSDB");
            var emds = new ApiDataDbContext(options.Options);
            emds.departments.Add(new Department { DepartmentId = 1, DepartmentName = "Com" });
            emds.departments.Add(new Department { DepartmentId = 2, DepartmentName = "test" });
            emds.employees.Add(new Employee { EmployeeId = 1, Name = "Saumil", Surname = "Patel", DepartmentId = 1 });
            emds.employees.Add(new Employee { EmployeeId = 2, Name = "Darshan", Surname = "Patel", DepartmentId = 2 });
            emds.SaveChanges();
            return emds;

        }
        [SetUp]
        public void Setup()
        {
            _context = GetContext();
            _context.Database.EnsureCreated();

        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }


        [Test]
        public async Task GetAsync_returns_record()
        {
            EmployeeController _controller = new EmployeeController(_context);
            var data = await _controller.GetAsync();
            Assert.IsInstanceOf<OkObjectResult>(data);
        }

        [Test]
        public async Task GetEmployeeByIdAsync_ValueIsValid_ReturnResults()
        {
            EmployeeController _controller = new EmployeeController(_context);
            var employee = await _controller.GetEmpByIdAsync("2");
            Assert.IsInstanceOf<OkObjectResult>(employee);
        }

        [Test]
        public async Task GetEmployeeByIdAsync_ValueIsInvalid_returnsNotFound()
        {
            EmployeeController _controller = new EmployeeController(_context);
            var employee = await _controller.GetEmpByIdAsync("300");
            Assert.IsInstanceOf<NotFoundResult>(employee);
        }

        [Test]
        public async Task PostAsync_NewEmployeeWithSameDepartmentId_AddAsyncEmployee()
        {
            EmployeeController _controller = new EmployeeController(_context);
            var emp = new Employee
            {
                EmployeeId = 3,
                Name = "Ashiyana",
                Surname = "Cant Pronounce",
                DepartmentId = 1
            };
            var employee = await _controller.PostAsync(emp);
            Assert.IsInstanceOf<CreatedResult>(employee);
        }
        [Test]
        public async Task PostAsync_NewEmployeewithWrongDepartmentId_ReturnBadRequest()
        {
            EmployeeController _controller = new EmployeeController(_context);
            var emp = new Employee
            {
                EmployeeId = 3,
                Name = "Ashiyana",
                Surname = "Cant Pronounce",
                DepartmentId = 2000
            };
            var employee = await _controller.PostAsync(emp);
            Assert.IsInstanceOf<BadRequestResult>(employee);
        }
        [Test]
        public async Task GetAsync_returns_recordDept()
        {
            DepartmentController _controller = new DepartmentController(_context);
            var data = await _controller.GetAsync();
            Assert.IsInstanceOf<OkObjectResult>(data);
        }

        [Test]
        public async Task GetDepartmentByIdAsync_ValueIsValid_ReturnResults()
        {
            DepartmentController _controller = new DepartmentController(_context);
            var employee = await _controller.GetByIdAsync("1");
            Assert.IsInstanceOf<OkObjectResult>(employee);
        }
        [Test]
        public async Task GetDepartmentByIdAsync_ValueIsInvalid_returnsNotFound()
        {
            DepartmentController _controller = new DepartmentController(_context);
            var employee = await _controller.GetByIdAsync("50");
            Assert.IsInstanceOf<NotFoundResult>(employee);
        }
    }
}