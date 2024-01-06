using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MottuShared.Cache.Options;
using MottuShared.Cache.Services.Implementation;
using MottuShared.Cache.Services.Interfaces;

namespace MottuShared.Cache
{
    public static class CacheStartup
    {
        public static void Config(IServiceCollection service, ConfigurationManager config, CacheStartupOptions options)
        {
            if (options.UseMemoryCache)
            {
                service.AddMemoryCache();
                service.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            }


            if (options.UseRedisCache)
            {
                service.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = config.GetConnectionString("RedisCache") ?? String.Empty;
                });

                service.AddSingleton<ICacheService, CacheService>();
            }
        }
    }
}
