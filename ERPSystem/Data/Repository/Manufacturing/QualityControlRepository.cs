using ERPSystem.Models.Manufacturing;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data.Repository.Manufacturing
{
    public class QualityControlRepository : IRepository<QualityControl>
    {
        private readonly ERPDbContext _context;

        public QualityControlRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QualityControl>> GetAllAsync()
        {
            return await _context.QualityControls
                .Include(qc => qc.Product) // Include navigation property if needed
                .ToListAsync();
        }

        public async Task<QualityControl?> GetByIdAsync(int id)
        {
            return await _context.QualityControls
                .Include(qc => qc.Product) // Include navigation property if needed
                .FirstOrDefaultAsync(qc => qc.Id == id);
        }

        public async Task AddAsync(QualityControl item)
        {
            _context.QualityControls.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QualityControl item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.QualityControls.FindAsync(id);
            if (item != null)
            {
                _context.QualityControls.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
