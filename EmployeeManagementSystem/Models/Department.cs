using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }
        
        //Navigation properties
        public virtual  List<Employee>? Employees { get; set; }

    }
}
