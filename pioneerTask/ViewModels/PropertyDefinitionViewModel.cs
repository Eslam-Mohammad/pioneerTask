using pioneerTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace pioneerTask.ViewModels
{
    public class PropertyDefinitionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Property Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Property Type")]
        public PropertyType Type { get; set; }

        [Display(Name = "Is Required?")]
        public bool IsRequired { get; set; }

        [Display(Name = "Dropdown Options")]
        public List<string>? Options { get; set; } = new List<string>();
    }
}
