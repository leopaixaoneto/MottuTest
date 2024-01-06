namespace MottuShared.Cache.Services.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellation = default)
            where T : class;

        Task<T?> GetAsync<T>(string key, Func<Task<T?>> factory, CancellationToken cancellation = default)
            where T : class;

        Task SetAsync<T>(string key, T value, CancellationToken cancellation = default)
            where T : class;
    }
}
