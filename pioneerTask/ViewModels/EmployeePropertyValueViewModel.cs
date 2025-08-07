using pioneerTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace pioneerTask.ViewModels
{
    public class EmployeePropertyValueViewModel
    {
        public int Id { get; set; }
       
        public string Value { get; set; }

        public int PropertyDefinitionId { get; set; }
        public string PropertyName { get; set; }
        public PropertyType PropertyType { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? Options { get; set; }
    }
}
