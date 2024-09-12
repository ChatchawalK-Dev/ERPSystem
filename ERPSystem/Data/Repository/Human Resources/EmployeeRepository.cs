using ERPSystem.Models.HumanResources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.HumanResources
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByPositionAsync(string position);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ERPDbContext _context;

        public EmployeeRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Recruitment)
                .Include(e => e.Trainings)
                .Include(e => e.TimeRecords)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Recruitment)
                .Include(e => e.Trainings)
                .Include(e => e.TimeRecords)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Employees.FindAsync(id);
            if (entity != null)
            {
                _context.Employees.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByPositionAsync(string position)
        {
            return await _context.Employees
                .Where(e => e.Position == position)
                .Include(e => e.Recruitment)
                .Include(e => e.Trainings)
                .Include(e => e.TimeRecords)
                .ToListAsync();
        }
    }
}
