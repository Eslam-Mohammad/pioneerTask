using System.ComponentModel.DataAnnotations;

namespace pioneerTask.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; } 
        public List<EmployeePropertyValue> PropertyValues { get; set; } = new List<EmployeePropertyValue>();
    }
}
