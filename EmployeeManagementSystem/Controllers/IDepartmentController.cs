using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public interface IDepartmentController
    {
        Task<IActionResult> GetAsync();
        Task<IActionResult> GetByIdAsync(string id);
        Task<IActionResult> PostAsync([FromBody] Department dept);
    }
}