using ERPSystem.Models.Document_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.DocumentM
{
    public interface IDocVersionRepository : IRepository<DocVersion>
    {
        Task<IEnumerable<DocVersion>> GetDocVersionsWithDocumentsAsync();
    }

    public class DocVersionRepository : IDocVersionRepository
    {
        private readonly ERPDbContext _context;

        public DocVersionRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocVersion>> GetAllAsync()
        {
            return await _context.DocVersions
                .Include(dv => dv.Document)
                .ToListAsync();
        }

        public async Task<DocVersion?> GetByIdAsync(int id)
        {
            return await _context.DocVersions
                .Include(dv => dv.Document)
                .FirstOrDefaultAsync(dv => dv.Id == id);
        }

        public async Task AddAsync(DocVersion entity)
        {
            await _context.DocVersions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocVersion entity)
        {
            _context.DocVersions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DocVersions.FindAsync(id);
            if (entity != null)
            {
                _context.DocVersions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DocVersion>> GetDocVersionsWithDocumentsAsync()
        {
            return await _context.DocVersions
                .Include(dv => dv.Document)
                .ToListAsync();
        }
    }
}
