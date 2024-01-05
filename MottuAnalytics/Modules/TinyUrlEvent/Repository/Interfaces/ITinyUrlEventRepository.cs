using MottuAnalytics.Database.Entities;

namespace MottuAnalytics.Modules.TinyUrlEvent.Repository.Interfaces
{
    public interface ITinyUrlEventRepository
    {
        public Task<List<TinyUrlEventEntity>?> GetEventsByTinyUrlIdAsync(string id, CancellationToken cancellationToken = default);

        public Task Add(TinyUrlEventEntity tinyUrlEvent);
    }
}
