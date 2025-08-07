using Microsoft.AspNetCore.Mvc;
using pioneerTask.Enums;
using pioneerTask.Interfaces.Services;
using pioneerTask.ViewModels;
using System.Threading.Tasks;

namespace pioneerTask.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPropertyDefinitionService _propertyDefinitionService;

        public EmployeesController(
            IEmployeeService employeeService,
            IPropertyDefinitionService propertyDefinitionService)
        {
            _employeeService = employeeService;
            _propertyDefinitionService = propertyDefinitionService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

     

        public async Task<IActionResult> Create()
        {
            var model = new EmployeeViewModel();
            var properties = await _propertyDefinitionService.GetAllDefinitionsAsync();

            model.Properties = properties.Select(p => new EmployeePropertyValueViewModel
            {
                PropertyDefinitionId = p.Id,
                PropertyName = p.Name,
                PropertyType = p.Type,
                IsRequired = p.IsRequired,
                Options = p.Type == PropertyType.Dropdown ? p.Options : null
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVm)
        {
           
            ModelState.Clear();

            
            var hasErrors = false;

            
            if (string.IsNullOrEmpty(employeeVm.Code))
            {
                ModelState.AddModelError("Code", "Code is required");
                hasErrors = true;
            }

            if (string.IsNullOrEmpty(employeeVm.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
                hasErrors = true;
            }

            
            if (employeeVm.Properties != null)
            {
                for (int i = 0; i < employeeVm.Properties.Count; i++)
                {
                    if (employeeVm.Properties[i].IsRequired &&
                        string.IsNullOrEmpty(employeeVm.Properties[i].Value))
                    {
                        ModelState.AddModelError($"Properties[{i}].Value", "This field is required");
                        hasErrors = true;
                    }
                }
            }

            if (!hasErrors)
            {
                await _employeeService.CreateEmployeeAsync(employeeVm);
                return RedirectToAction(nameof(Index));
            }

            
            var allDefinitions = await _propertyDefinitionService.GetAllDefinitionsAsync();
            foreach (var prop in employeeVm.Properties)
            {
                var definition = allDefinitions.FirstOrDefault(d => d.Id == prop.PropertyDefinitionId);
                if (definition != null && definition.Type == PropertyType.Dropdown)
                {
                    prop.Options = definition.Options;
                }
            }

            return View(employeeVm);
        }


        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeWithPropertiesAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeWithPropertiesAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

           
            var allDefinitions = await _propertyDefinitionService.GetAllDefinitionsAsync();

          
            foreach (var definition in allDefinitions)
            {
                if (!employee.Properties.Any(p => p.PropertyDefinitionId == definition.Id))
                {
                    employee.Properties.Add(new EmployeePropertyValueViewModel
                    {
                        PropertyDefinitionId = definition.Id,
                        PropertyName = definition.Name,
                        PropertyType = definition.Type,
                        IsRequired = definition.IsRequired,
                        Options = definition.Type == PropertyType.Dropdown ? definition.Options : null
                    });
                }
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
            {
                return NotFound();
            }

           
            ModelState.Clear();

            
            var hasErrors = false;

            
            if (string.IsNullOrEmpty(employeeVm.Code))
            {
                ModelState.AddModelError("Code", "Code is required");
                hasErrors = true;
            }

            if (string.IsNullOrEmpty(employeeVm.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
                hasErrors = true;
            }

           
            if (employeeVm.Properties != null)
            {
                for (int i = 0; i < employeeVm.Properties.Count; i++)
                {
                    if (employeeVm.Properties[i].IsRequired &&
                        string.IsNullOrEmpty(employeeVm.Properties[i].Value))
                    {
                        ModelState.AddModelError($"Properties[{i}].Value", "This field is required");
                        hasErrors = true;
                    }
                }
            }

            if (!hasErrors)
            {
                await _employeeService.UpdateEmployeeWithPropertiesAsync(employeeVm);
                return RedirectToAction(nameof(Index));
            }

            
            var allDefinitions = await _propertyDefinitionService.GetAllDefinitionsAsync();
            foreach (var prop in employeeVm.Properties)
            {
                var definition = allDefinitions.FirstOrDefault(d => d.Id == prop.PropertyDefinitionId);
                if (definition != null && definition.Type == PropertyType.Dropdown)
                {
                    prop.Options = definition.Options;
                }
            }

            return View(employeeVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeWithPropertiesAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeWithPropertiesAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}