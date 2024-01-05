using MottuAnalytics.Database.Entities;
using MottuAnalytics.Modules.TinyUrlEvent.DTO;
using MottuAnalytics.Modules.TinyUrlEvent.Repository.Interfaces;

namespace MottuAnalytics.Modules.TinyUrlEvent.Services
{
    public class GetTinyUrlAnalyticsAction
    {
        private readonly ITinyUrlEventRepository _repository;

        public GetTinyUrlAnalyticsAction(ITinyUrlEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TinyUrlEventEntity>?> Execute(GetTinyUrlAnalyticsRequest payload)
        {
            var result = await _repository.GetEventsByTinyUrlIdAsync(payload.TinyUrlId.ToString());

            return result;
        }
    }

    
}
