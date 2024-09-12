using ERPSystem.Models.Document_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.DocumentM
{
    public interface IAccessControlRepository : IRepository<AccessControl>
    {
        Task<IEnumerable<AccessControl>> GetAccessControlsByUserIdAsync(int userId);
        Task<IEnumerable<AccessControl>> GetAccessControlsByDocumentIdAsync(int documentId);
    }

    public class AccessControlRepository : IAccessControlRepository
    {
        private readonly ERPDbContext _context;

        public AccessControlRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccessControl>> GetAllAsync()
        {
            return await _context.AccessControls
                .Include(a => a.Document)
                .ToListAsync();
        }

        public async Task<AccessControl?> GetByIdAsync(int id)
        {
            return await _context.AccessControls
                .Include(a => a.Document)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AccessControl entity)
        {
            await _context.AccessControls.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccessControl entity)
        {
            _context.AccessControls.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AccessControls.FindAsync(id);
            if (entity != null)
            {
                _context.AccessControls.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AccessControl>> GetAccessControlsByUserIdAsync(int userId)
        {
            return await _context.AccessControls
                .Where(a => a.UserID == userId)
                .Include(a => a.Document)
                .ToListAsync();
        }

        public async Task<IEnumerable<AccessControl>> GetAccessControlsByDocumentIdAsync(int documentId)
        {
            return await _context.AccessControls
                .Where(a => a.DocumentID == documentId)
                .Include(a => a.Document)
                .ToListAsync();
        }
    }
}
