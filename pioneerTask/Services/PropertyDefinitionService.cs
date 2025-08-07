using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pioneerTask.Enums;
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
    public class PropertyDefinitionService : IPropertyDefinitionService
    {
        private readonly IPropertyDefinitionRepository _repository;
        private readonly IMapper _mapper;

        public PropertyDefinitionService(
            IPropertyDefinitionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyDefinitionViewModel>> GetAllDefinitionsAsync()
        {
            var definitions = await _repository.GetAllAsync();
            return definitions.Select(definition =>
            {
                var vm = _mapper.Map<PropertyDefinitionViewModel>(definition);
                if (definition.Type == PropertyType.Dropdown)
                {
                    vm.Options = definition.DropdownOptions?.Select(o => o.Value).ToList();
                }
                return vm;
            });
        }

        public async Task<PropertyDefinitionViewModel> GetDefinitionByIdAsync(int id)
        {
            var definition = await _repository.GetByIdAsync(id);
            var vm = _mapper.Map<PropertyDefinitionViewModel>(definition);
            if (definition.Type == PropertyType.Dropdown)
            {
                vm.Options = definition.DropdownOptions?.Select(o => o.Value).ToList();
            }
            return vm;
        }

        public async Task CreateDefinitionAsync(PropertyDefinitionViewModel definitionVm)
        {
            var definition = _mapper.Map<EmployeePropertyDefinition>(definitionVm);

            if (definition.Type == PropertyType.Dropdown && definitionVm.Options != null)
            {
                definition.DropdownOptions = definitionVm.Options
                    .Where(o => !string.IsNullOrWhiteSpace(o))
                    .Select(o => new DropdownOption { Value = o })
                    .ToList();
            }

            await _repository.AddAsync(definition);
        }

        public async Task UpdateDefinitionAsync(PropertyDefinitionViewModel definitionVm)
        {
            var existingDefinition = await _repository.GetByIdAsync(definitionVm.Id);

         
            existingDefinition.Name = definitionVm.Name;
            existingDefinition.Type = definitionVm.Type;
            existingDefinition.IsRequired = definitionVm.IsRequired;

           
            if (existingDefinition.Type == PropertyType.Dropdown)
            {
            
             await  _repository.RemoveDropdownOptionsAsync(existingDefinition.Id);

              
                existingDefinition.DropdownOptions = definitionVm.Options?
                    .Where(o => !string.IsNullOrWhiteSpace(o))
                    .Select(o => new DropdownOption { Value = o })
                    .ToList() ?? new List<DropdownOption>();
            }
            else
            {
            
                if (existingDefinition.DropdownOptions?.Any() == true)
                {
                    await _repository.RemoveDropdownOptionsAsync(existingDefinition.Id);
                    existingDefinition.DropdownOptions = null;
                }
            }

            await _repository.UpdateAsync(existingDefinition);
        }

        public async Task DeleteDefinitionAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}