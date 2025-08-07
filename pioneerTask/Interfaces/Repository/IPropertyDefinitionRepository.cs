using pioneerTask.Models;

namespace pioneerTask.Interfaces.Repository
{
    public interface IPropertyDefinitionRepository
    {
        Task<IEnumerable<EmployeePropertyDefinition>> GetAllAsync();
        Task<EmployeePropertyDefinition> GetByIdAsync(int id);
        Task AddAsync(EmployeePropertyDefinition definition);
        Task UpdateAsync(EmployeePropertyDefinition definition);
        Task DeleteAsync(int id);
        Task RemoveDropdownOptionsAsync(int definitionId);
    }
}
