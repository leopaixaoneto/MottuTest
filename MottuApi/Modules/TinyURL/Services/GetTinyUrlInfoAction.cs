using MottuApi.Modules.TinyURL.DTO;
using MottuTest.Database.Entities;
using MottuTest.Modules.TinyURL.Repository.Interfaces;

namespace MottuApi.Modules.TinyURL.Services
{
    public class TinyUrlEntityWithAnalytics : TinyUrlEntity
    {
        public long Views { get; set; }
        public DateTime LastViewedAt { get; set; }
    }

    public class GetTinyUrlInfoAction
    {
        private readonly ITinyUrlRepository _repository;
        private readonly GetTinyUrlAnalyticsAction _getTinyUrlAnalyticsAction;

        public GetTinyUrlInfoAction(ITinyUrlRepository repository, GetTinyUrlAnalyticsAction getTinyUrlAnalyticsAction)
        {
            _repository = repository;
            _getTinyUrlAnalyticsAction = getTinyUrlAnalyticsAction;
        }

        public async Task<TinyUrlEntityWithAnalytics?> Execute(GetTinyUrlInfoRequest payload)
        {
            var TinyUrlTask = _repository.GetById(payload.Id);
            var analyticsTask = _getTinyUrlAnalyticsAction.Execute(payload.Id);
        
            await Task.WhenAll(TinyUrlTask, analyticsTask);

            var tinyUrlInfo = await TinyUrlTask;
            var analytics = await analyticsTask;
            
            if(tinyUrlInfo is not null)
            {
                return new TinyUrlEntityWithAnalytics
                {
                    Id = tinyUrlInfo.Id,
                    CreateAt = tinyUrlInfo.CreateAt,
                    Original = tinyUrlInfo.Original,
                    Shortened = tinyUrlInfo.Shortened,
                    ShortenedCode = tinyUrlInfo.ShortenedCode,
                    Views = analytics.Views,
                    LastViewedAt = analytics.LastViewedAt,
                };
            }
            else
            {
                throw new InvalidDataException();
            }

        }
    }
}
