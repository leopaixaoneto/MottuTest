using Microsoft.Extensions.Caching.Distributed;
using MottuShared.Cache.Services.Interfaces;
using Newtonsoft.Json;

namespace MottuShared.Cache.Services.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _entryOptions;
        
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _entryOptions = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellation = default) where T : class
        {
            string? data = await _distributedCache.GetStringAsync(key, cancellation);

            return (data is null) ? null : JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T?> GetAsync<T>(string key, Func<Task<T?>> factory, CancellationToken cancellation = default) where T : class
        {
            T? data = await GetAsync<T>(key, cancellation);

            if(data is null)
            {
                data = await factory();

                if(data is not null)
                    _ = SetAsync(key, data, cancellation);
            }

            return data;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellation = default) where T : class
        {
            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(value),
                _entryOptions,
                cancellation);
        }

    }
}
