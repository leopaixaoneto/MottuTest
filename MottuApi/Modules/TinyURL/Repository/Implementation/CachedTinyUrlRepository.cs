using MottuTest.Database.Entities;
using MottuTest.Modules.TinyURL.Repository.Interfaces;
using MottuShared.Cache.Services.Interfaces;

namespace MottuTest.Modules.TinyURL.Repository.Implementation
{
    public class CachedTinyUrlRepository : ITinyUrlRepository
    {
        private readonly TinyUrlRepository _repository;
        private readonly ICacheService _cacheService;

        public CachedTinyUrlRepository(TinyUrlRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task Add(TinyUrlEntity tinyUrl)
        {
            await _repository.Add(tinyUrl);

            //TO-DO Analyze the necessity of cache the inserted data
            _ = _cacheService.SetAsync(tinyUrl.ShortenedCode, tinyUrl);
        }

        public async Task<TinyUrlEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            //Getting Cached value otherwise create and cache
            return await _cacheService.GetAsync(
                id.ToString(),
                async () =>
                {
                    return await _repository.GetById(id, cancellationToken);
                },
                cancellationToken);
        }

        public async Task<TinyUrlEntity?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            //Getting Cached value otherwise create and cache
            return await _cacheService.GetAsync(
                shortCode,
                async () =>
                {
                    return await _repository.GetByShortCodeAsync(shortCode, cancellationToken);
                },
                cancellationToken);
        }
    }
}
