using MottuKGS.Modules.GeneratedKey.Repository.Implementation;
using MottuShared.Cache.Services.Interfaces;
using MottuShared.Logger;
using Quartz;

namespace MottuKGS.Modules.GeneratedKey.Jobs
{
    [DisallowConcurrentExecution]
    public class InMemoryAvailabilityCheckRoutine : IJob
    {
        private readonly ILogger<InMemoryAvailabilityCheckRoutine> _logger;
        private readonly IMemoryCacheService _cacheService;
        private readonly GeneratedKeyRepository _respository;

        public InMemoryAvailabilityCheckRoutine(ILogger<InMemoryAvailabilityCheckRoutine> logger, IMemoryCacheService cacheService, GeneratedKeyRepository respository)
        {
            _logger = logger;
            _cacheService = cacheService;
            _respository = respository;

            if (!_cacheService.Contains("cachedKeys"))
            {
                _cacheService.SetInMemory<List<string>>("cachedKeys", new());
            }
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogSimpleMessage($"{DateTime.Now} - Checking for inMemory availability of generated keys");
            
            var actualInMemory = _cacheService.GetInMemory<List<string>>("cachedKeys");

            if(actualInMemory?.Count < 10)
            {
                var newKeys = await _respository.GetBulk(10);

                actualInMemory.AddRange(newKeys.Select(k => k.Key));
                _cacheService.SetInMemory("cachedKeys", actualInMemory);

                await _respository.UpdateUsedKeys(newKeys);
            }

        }
    }
}
