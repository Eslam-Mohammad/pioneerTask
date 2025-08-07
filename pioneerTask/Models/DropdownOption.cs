using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pioneerTask.Models
{
    public class DropdownOption
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Value { get; set; }
        [ForeignKey("EmployeePropertyDefinition")]
        public int EmployeePropertyDefinitionId { get; set; }
        public EmployeePropertyDefinition EmployeePropertyDefinition { get; set; }
    }
}
