using MottuAnalytics.Database.Entities;
using MottuShared.Cache.Services.Interfaces;
using MottuAnalytics.Modules.TinyUrlEvent.Repository.Interfaces;
using MottuAnalytics.Modules.TinyUrlEvent.Enums;

namespace MottuAnalytics.Modules.TinyUrlEvent.Repository.Implementation
{
    public class CachedTinyUrlEventRepository : ITinyUrlEventRepository
    {
        private readonly TinyUrlEventRepository _repository;
        private readonly IMemoryCacheService _cacheService;

        public CachedTinyUrlEventRepository(TinyUrlEventRepository repository, IMemoryCacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task Add(TinyUrlEventEntity tinyUrlEvent)
        {
            await _repository.Add(tinyUrlEvent);


            //TO-DO see the necessity to cache this
            string key = $"base-{tinyUrlEvent.TinyUrlId}";

            if (tinyUrlEvent.Type != EventType.Created && _cacheService.Contains(key))
            {
                var actualCache = _cacheService.GetInMemory<List<TinyUrlEventEntity>>(key);
                if(actualCache is not null)
                {
                    actualCache.Add(tinyUrlEvent);
                    _cacheService.SetInMemory(key, actualCache);
                }
            }
        }

        public async Task<List<TinyUrlEventEntity>?> GetEventsByTinyUrlIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _cacheService.GetInMemory(
                $"base-{id}",
                async () =>
                {
                    return await _repository.GetEventsByTinyUrlIdAsync(id, cancellationToken);
                });
        }
    }
}
