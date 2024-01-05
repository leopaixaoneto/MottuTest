using Microsoft.EntityFrameworkCore;
using MottuTest.Database;
using MottuTest.Database.Entities;
using MottuTest.Modules.TinyURL.Repository.Interfaces;

namespace MottuTest.Modules.TinyURL.Repository.Implementation
{
    public class TinyUrlRepository : ITinyUrlRepository
    {
        private readonly DatabaseContext _context;

        public TinyUrlRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(TinyUrlEntity tinyUrl)
        {
            _context.TinyUrls.Add(tinyUrl);
            await _context.SaveChangesAsync();
        }

        public async Task<TinyUrlEntity?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            return await _context.TinyUrls
                .FirstOrDefaultAsync(t => t.ShortenedCode == shortCode, cancellationToken);
        }

        public async Task<TinyUrlEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.TinyUrls
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}
