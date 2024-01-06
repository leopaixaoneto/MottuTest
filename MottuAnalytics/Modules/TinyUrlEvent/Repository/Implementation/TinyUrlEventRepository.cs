using Microsoft.EntityFrameworkCore;
using MottuAnalytics.Database;
using MottuAnalytics.Database.Entities;
using MottuAnalytics.Modules.TinyUrlEvent.Repository.Interfaces;

namespace MottuAnalytics.Modules.TinyUrlEvent.Repository.Implementation
{
    public class TinyUrlEventRepository : ITinyUrlEventRepository
    {
        private readonly DatabaseContext _context;
        public TinyUrlEventRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(TinyUrlEventEntity tinyUrlEvent)
        {
            _context.TinyUrlEvents.Add(tinyUrlEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TinyUrlEventEntity>?> GetEventsByTinyUrlIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.TinyUrlEvents.Where(t => t.TinyUrlId.ToString() == id).ToListAsync(cancellationToken);
        }
    }
}
