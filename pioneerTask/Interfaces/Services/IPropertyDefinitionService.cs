using pioneerTask.ViewModels;

namespace pioneerTask.Interfaces.Services
{
    public interface IPropertyDefinitionService
    {
        Task<IEnumerable<PropertyDefinitionViewModel>> GetAllDefinitionsAsync();
        Task<PropertyDefinitionViewModel> GetDefinitionByIdAsync(int id);
        Task CreateDefinitionAsync(PropertyDefinitionViewModel definition);
        Task UpdateDefinitionAsync(PropertyDefinitionViewModel definition);
        Task DeleteDefinitionAsync(int id);
    }
}
