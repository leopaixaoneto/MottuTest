using MassTransit;
using MottuShared.Cache.Services.Interfaces;
using MottuShared.Contracts.Requests;
using MottuShared.Contracts.Responses;

namespace MottuKGS.Modules.GeneratedKey.Consumers
{
    public class GetGeneratedKeyConsumer : IConsumer<GetGeneratedKeyRequest>
    {
        private readonly IMemoryCacheService _cacheService;

        public GetGeneratedKeyConsumer(IMemoryCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<GetGeneratedKeyRequest> context)
        {
            var response = new GetGeneratedKeyResponse();

            var cachedKeys = _cacheService.GetInMemory<List<string>>("cachedKeys");
            
            if (cachedKeys != null)
            {
                var key = cachedKeys.FirstOrDefault();
                if(key is not null)
                {
                    response.GeneratedKeys.Add(key);
                    cachedKeys.RemoveAt(0);
                    _cacheService.SetInMemory("cachedKeys", cachedKeys);
                }
            }

            await context.RespondAsync(response);
        }
    }
}
