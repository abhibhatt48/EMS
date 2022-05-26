using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required] 
        //navigation properties
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public virtual Department? Department { get; set; }
    }
}
