using MottuAnalytics.Database.Entities;
using MottuAnalytics.Modules.TinyUrlEvent.DTO;
using MottuAnalytics.Modules.TinyUrlEvent.Repository.Interfaces;


namespace MottuAnalytics.Modules.TinyUrlEvent.Services
{
    public class CreateTinyUrlEventAction 
    {
        private readonly ITinyUrlEventRepository _repository;

        public CreateTinyUrlEventAction(ITinyUrlEventRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(CreateTinyUrlEventRequest payload)
        {
            var entity = new TinyUrlEventEntity()
            {
               Id = Guid.NewGuid(),
               TinyUrlId = payload.TinyUrlId,
               CreatedAt = payload.CreatedAt,
               Type = payload.EventType
            };

            await _repository.Add(entity);
        }
    }
}
