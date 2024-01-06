namespace MottuShared.Cache.Options
{
    public record CacheConfiguration
    {
        public bool EnabledExpiration { get; set; }
        public int ExpirationTimeInSeconds { get; set; } = 5;
    }
}
