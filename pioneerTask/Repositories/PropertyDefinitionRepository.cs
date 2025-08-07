using Microsoft.EntityFrameworkCore;
using pioneerTask.Interfaces.Repository;
using pioneerTask.Models;

namespace pioneerTask.Repositories
{
    public class PropertyDefinitionRepository : IPropertyDefinitionRepository
    {
        public  AppDbContext _context;

        public PropertyDefinitionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RemoveDropdownOptionsAsync(int definitionId)
        {
            var options = await _context.DropdownOptions
                .Where(o => o.EmployeePropertyDefinitionId == definitionId)
                .ToListAsync();

            _context.DropdownOptions.RemoveRange(options);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(EmployeePropertyDefinition definition)
        {
            await _context.EmployeePropertyDefinitions.AddAsync(definition);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var definition = await _context.EmployeePropertyDefinitions.FindAsync(id);
            if (definition != null)
            {
                _context.EmployeePropertyDefinitions.Remove(definition);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<EmployeePropertyDefinition>> GetAllAsync()
        {
            return await _context.EmployeePropertyDefinitions
                .Include(d => d.DropdownOptions)
                .ToListAsync();

        }

        public async Task<EmployeePropertyDefinition> GetByIdAsync(int id)
        {
            return await _context.EmployeePropertyDefinitions
                .Include(d => d.DropdownOptions)
                .FirstOrDefaultAsync(d => d.Id == id);

        }

        public async Task UpdateAsync(EmployeePropertyDefinition definition)
        {
            
                _context.EmployeePropertyDefinitions.Update(definition);
            await _context.SaveChangesAsync();
            }

        }
    }

