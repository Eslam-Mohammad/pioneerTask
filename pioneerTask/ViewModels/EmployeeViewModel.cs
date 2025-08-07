using System.ComponentModel.DataAnnotations;

namespace pioneerTask.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public List<EmployeePropertyValueViewModel> Properties { get; set; } = new();
    }
}
