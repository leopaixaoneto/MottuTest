using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MottuShared.Cache.Options;
using MottuShared.Cache.Services.Interfaces;


namespace MottuShared.Cache.Services.Implementation
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _distributedCache;
        private readonly MemoryCacheEntryOptions _entryOptions;
        
        public MemoryCacheService(IMemoryCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _entryOptions = new MemoryCacheEntryOptions();

            CacheConfiguration cacheConfig = new();
            configuration.GetSection("CacheConfiguration").Bind(cacheConfig);

            if (cacheConfig.EnabledExpiration)
            {
                _entryOptions.SlidingExpiration = TimeSpan.FromSeconds(cacheConfig.ExpirationTimeInSeconds);
            }
        }

        public async Task<T?> GetInMemory<T>(string key, Func<Task<T?>> factory) where T : class
        {
            T? data = GetInMemory<T>(key);

            if(data is null)
            {
                data = await factory();

                if(data is not null)
                    SetInMemory(key, data);
            }

            return data;
        }

        public T? GetInMemory<T>(string key) where T : class
        {
            return _distributedCache.Get<T>(key);
        }

        public void SetInMemory<T>(string key, T value) where T : class
        {
            _distributedCache.Set(key, value, _entryOptions);
        }

        public bool Contains(string key)
        {
            _distributedCache.TryGetValue(key, out var cachedValue);

            return cachedValue is not null;
        }

        public void RemoveKeyInMemory(string key) { 
            _distributedCache.Remove(key);
        }
    }
}
