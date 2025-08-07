using pioneerTask.ViewModels;

namespace pioneerTask.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesAsync();
        Task<EmployeeViewModel> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(EmployeeViewModel employee);
        Task UpdateEmployeeAsync(EmployeeViewModel employee);
        Task DeleteEmployeeAsync(int id);
        Task<EmployeeViewModel> GetEmployeeWithPropertiesAsync(int id);
        Task UpdateEmployeeWithPropertiesAsync(EmployeeViewModel employeeVm);
        Task DeleteEmployeeWithPropertiesAsync(int id);
    }
}
