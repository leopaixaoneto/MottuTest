using MassTransit;
using MottuApi.Modules.TinyURL.Services;
using MottuShared.Contracts;
using MottuShared.Contracts.Events;
using MottuTest.Database;
using MottuTest.Database.Entities;
using MottuTest.Modules.TinyURL.DTO;
using MottuTest.Modules.TinyURL.Repository.Implementation;
using MottuTest.Modules.TinyURL.Repository.Interfaces;
using System;

namespace MottuTest.Modules.TinyURL.Services
{
    public class CreateTinyUrlAction
    {
        private readonly ITinyUrlRepository _repository;
        private readonly GetGeneratedKeyAction _getGeneratedKeyAction;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateTinyUrlAction(ITinyUrlRepository repository, GetGeneratedKeyAction getGeneratedKeyAction, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _getGeneratedKeyAction = getGeneratedKeyAction;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<TinyUrlEntity> Execute(CreateTinyUrlRequest payload, HttpContext httpContext)
        {
            //Checks if the payload has a valid URL
            if(!Uri.TryCreate(payload.Url, UriKind.Absolute, out _)) 
                throw new InvalidCastException();

            string code = await _getGeneratedKeyAction.Execute().ConfigureAwait(false);

            var shortenedUrl = new TinyUrlEntity
            {
                Id = Guid.NewGuid(),
                Original = payload.Url,
                ShortenedCode = code,
                Shortened = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{code}",
                CreateAt = DateTime.UtcNow
            };

            await _repository.Add(shortenedUrl);

            //Dispatch the Created event
            _ = _publishEndpoint.Publish(new TinyUrlCreatedEvent()
            {
                Id = shortenedUrl.Id,
                CreatedAt = DateTime.UtcNow
            });

            return shortenedUrl;
        }
    }
}
