using System.ComponentModel.DataAnnotations.Schema;

namespace pioneerTask.Models
{
    public class EmployeePropertyValue
    {
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("PropertyDefinition")]
        public int PropertyDefinitionId { get; set; }
        public EmployeePropertyDefinition PropertyDefinition { get; set; }
        public string Value { get; set; } 
    }
}
