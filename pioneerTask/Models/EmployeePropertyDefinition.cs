using pioneerTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace pioneerTask.Models
{
    public class EmployeePropertyDefinition
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<DropdownOption>? DropdownOptions { get; set; } = new List<DropdownOption>();

        
    }
}
