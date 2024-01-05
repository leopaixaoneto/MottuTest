namespace MottuShared.Cache.Options
{
    public class CacheStartupOptions
    {
        public bool UseMemoryCache { get; set; }

        public bool UseRedisCache { get; set; }

        public CacheStartupOptions(bool bUseRedisCache, bool bUseMemoryCache)
        {
            this.UseRedisCache = bUseRedisCache;
            this.UseMemoryCache = bUseMemoryCache;
        }

        public CacheStartupOptions() { }
    }
}
