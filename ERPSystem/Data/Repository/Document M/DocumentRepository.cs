using ERPSystem.Models.Document_M;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Data.Repository.DocumentM
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsWithAccessControlsAsync();
        Task<IEnumerable<Document>> GetDocumentsWithVersionsAsync();
    }

    public class DocumentRepository : IDocumentRepository
    {
        private readonly ERPDbContext _context;

        public DocumentRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _context.Documents
                .Include(d => d.DocVersions)
                .Include(d => d.AccessControls)
                .ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents
                .Include(d => d.DocVersions)
                .Include(d => d.AccessControls)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Document entity)
        {
            await _context.Documents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document entity)
        {
            _context.Documents.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Documents.FindAsync(id);
            if (entity != null)
            {
                _context.Documents.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Document>> GetDocumentsWithAccessControlsAsync()
        {
            return await _context.Documents
                .Include(d => d.AccessControls)
                .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsWithVersionsAsync()
        {
            return await _context.Documents
                .Include(d => d.DocVersions)
                .ToListAsync();
        }
    }
}
