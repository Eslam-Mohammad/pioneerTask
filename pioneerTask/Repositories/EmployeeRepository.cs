using Microsoft.EntityFrameworkCore;
using pioneerTask.Interfaces.Repository;
using pioneerTask.Models;

namespace pioneerTask.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee =await  _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                 _context.SaveChangesAsync();
            }
           
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.PropertyValues)
                .ThenInclude(pv => pv.PropertyDefinition)
                .ToListAsync();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            return _context.Employees
                .Include(e => e.PropertyValues)
                .ThenInclude(pv => pv.PropertyDefinition)
                .FirstOrDefaultAsync(e => e.Id == id);

        }

        public Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return _context.SaveChangesAsync();

        }
    }
}
