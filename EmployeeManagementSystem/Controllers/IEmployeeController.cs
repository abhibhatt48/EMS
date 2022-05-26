using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public interface IEmployeeController
    {
        Task<IActionResult> GetAsync();
        Task<IActionResult> GetEmpByIdAsync(string id);
        Task<IActionResult> PostAsync([FromBody] Employee emp);
    }
}