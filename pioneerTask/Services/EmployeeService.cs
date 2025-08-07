using AutoMapper;
using pioneerTask.Interfaces;
using pioneerTask.Interfaces.Repository;
using pioneerTask.Interfaces.Services;
using pioneerTask.Models;
using pioneerTask.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pioneerTask.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPropertyDefinitionRepository _propertyDefinitionRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IPropertyDefinitionRepository propertyDefinitionRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _propertyDefinitionRepository = propertyDefinitionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

           
            foreach (var employee in employees)
            {
                if (employee.PropertyValues != null)
                {
                    foreach (var value in employee.PropertyValues)
                    {
                        value.PropertyDefinition = await _propertyDefinitionRepository.GetByIdAsync(value.PropertyDefinitionId);
                    }
                }
            }

            return _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

           
            if (employee.PropertyValues != null)
            {
                foreach (var value in employee.PropertyValues)
                {
                    value.PropertyDefinition = await _propertyDefinitionRepository.GetByIdAsync(value.PropertyDefinitionId);
                }
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        public async Task CreateEmployeeAsync(EmployeeViewModel employeeVm)
        {
            var employee = _mapper.Map<Employee>(employeeVm);

            if (employeeVm.Properties != null)
            {
                employee.PropertyValues = employeeVm.Properties
                    .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                    .Select(p => new EmployeePropertyValue
                    {
                        PropertyDefinitionId = p.PropertyDefinitionId,
                        Value = p.Value
                    })
                    .ToList();
            }

            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(EmployeeViewModel employeeVm)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeVm.Id);

           
            existingEmployee.Code = employeeVm.Code;
            existingEmployee.Name = employeeVm.Name;

            
            if (employeeVm.Properties != null)
            {
                existingEmployee.PropertyValues ??= new List<EmployeePropertyValue>();

               
                var valuesToRemove = existingEmployee.PropertyValues
                    .Where(v => !employeeVm.Properties.Any(p =>
                        p.PropertyDefinitionId == v.PropertyDefinitionId))
                    .ToList();

                foreach (var value in valuesToRemove)
                {
                    existingEmployee.PropertyValues.Remove(value);
                }

               
                foreach (var property in employeeVm.Properties)
                {
                    var existingValue = existingEmployee.PropertyValues
                        .FirstOrDefault(v => v.PropertyDefinitionId == property.PropertyDefinitionId);

                    if (existingValue != null)
                    {
                        existingValue.Value = property.Value;
                    }
                    else if (!string.IsNullOrWhiteSpace(property.Value))
                    {
                        existingEmployee.PropertyValues.Add(new EmployeePropertyValue
                        {
                            PropertyDefinitionId = property.PropertyDefinitionId,
                            Value = property.Value
                        });
                    }
                }
            }

            await _employeeRepository.UpdateAsync(existingEmployee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }


      
        public async Task<EmployeeViewModel> GetEmployeeWithPropertiesAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null) return null;

            
            if (employee.PropertyValues != null)
            {
                foreach (var value in employee.PropertyValues)
                {
                    value.PropertyDefinition = await _propertyDefinitionRepository.GetByIdAsync(value.PropertyDefinitionId);
                }
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        public async Task UpdateEmployeeWithPropertiesAsync(EmployeeViewModel employeeVm)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeVm.Id);

         
            existingEmployee.Code = employeeVm.Code;
            existingEmployee.Name = employeeVm.Name;

          
            existingEmployee.PropertyValues ??= new List<EmployeePropertyValue>();

            var valuesToRemove = existingEmployee.PropertyValues
                .Where(v => !employeeVm.Properties.Any(p =>
                    p.PropertyDefinitionId == v.PropertyDefinitionId))
                .ToList();

            foreach (var value in valuesToRemove)
            {
                existingEmployee.PropertyValues.Remove(value);
            }

            foreach (var property in employeeVm.Properties)
            {
                var existingValue = existingEmployee.PropertyValues
                    .FirstOrDefault(v => v.PropertyDefinitionId == property.PropertyDefinitionId);

                if (existingValue != null)
                {
                    existingValue.Value = property.Value;
                }
                else if (!string.IsNullOrWhiteSpace(property.Value))
                {
                    existingEmployee.PropertyValues.Add(new EmployeePropertyValue
                    {
                        PropertyDefinitionId = property.PropertyDefinitionId,
                        Value = property.Value
                    });
                }
            }

            await _employeeRepository.UpdateAsync(existingEmployee);
        }

        public async Task DeleteEmployeeWithPropertiesAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

    }
}