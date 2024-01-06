namespace MottuShared.Cache.Services.Interfaces
{
    public interface IMemoryCacheService
    {
        T? GetInMemory<T>(string key)
            where T : class;

        public Task<T?> GetInMemory<T>(string key, Func<Task<T?>> factory) 
            where T : class;
        
        void SetInMemory<T>(string key, T value)
            where T : class;

        public bool Contains(string key);

        public void RemoveKeyInMemory(string key);
    }
}
