using MassTransit;
using MottuShared.Contracts.Events;
using MottuTest.Database.Entities;
using MottuTest.Modules.TinyURL.DTO;
using MottuTest.Modules.TinyURL.Repository.Interfaces;

namespace MottuTest.Modules.TinyURL.Services
{
    public class GetTinyUrlAction
    {
        private readonly ITinyUrlRepository _repository;
        private readonly IPublishEndpoint _endpoint;

        public GetTinyUrlAction(ITinyUrlRepository repository, IPublishEndpoint endpoint)
        {
            _repository = repository;
            _endpoint = endpoint;
        }

        public async Task<TinyUrlEntity> Execute(GetTinyUrlRequest payload)
        {
            var tinyUrl = 
                await _repository.GetByShortCodeAsync(payload.ShortenedCode) 
                ?? throw new InvalidDataException();

            //Dispatch the Viewed event
            _ = _endpoint.Publish(new TinyUrlViewedEvent()
            {
                Id = tinyUrl.Id,
                ViewedAt = DateTime.UtcNow
            });
  
            return tinyUrl;
        }
    }
}
