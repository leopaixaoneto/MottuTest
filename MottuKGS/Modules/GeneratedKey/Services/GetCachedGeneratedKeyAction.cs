using MottuShared.Cache.Services.Interfaces;

namespace MottuKGS.Modules.GeneratedKey.Services
{
    public class GetCachedGeneratedKeyAction
    {
        private readonly IMemoryCacheService _cacheService;

        public GetCachedGeneratedKeyAction(IMemoryCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public string Execute()
        {
            var key = String.Empty;

            var cachedKeys = _cacheService.GetInMemory<List<string>>("cachedKeys");

            if(cachedKeys != null)
            {
                key = cachedKeys.FirstOrDefault() ?? "";

                cachedKeys.RemoveRange(0, 1);

                _cacheService.SetInMemory("cachedKeys", cachedKeys);
            }

            return key;
        }
    }
}
