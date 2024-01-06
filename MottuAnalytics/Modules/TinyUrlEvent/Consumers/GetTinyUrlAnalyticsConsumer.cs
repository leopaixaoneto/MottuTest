using MassTransit;
using MottuAnalytics.Modules.TinyUrlEvent.DTO;
using MottuAnalytics.Modules.TinyUrlEvent.Enums;
using MottuAnalytics.Modules.TinyUrlEvent.Services;
using MottuShared.Contracts.Requests;

namespace MottuAnalytics.Modules.TinyUrlEvent.Consumers
{
    public class GetTinyUrlAnalyticsConsumer : IConsumer<GetAnalyticsRequest>
    {
        private readonly GetTinyUrlAnalyticsAction _getTinyUrlAnalyticsUseCase;

        public GetTinyUrlAnalyticsConsumer(GetTinyUrlAnalyticsAction getTinyUrlAnalyticsUseCase)
        {
            _getTinyUrlAnalyticsUseCase = getTinyUrlAnalyticsUseCase;
        }

        public async Task Consume(ConsumeContext<GetAnalyticsRequest> context)
        {
            var analytics = await _getTinyUrlAnalyticsUseCase.Execute(new GetTinyUrlAnalyticsRequest
            {
                TinyUrlId = context.Message.Id
            });

            if(analytics is not null)
            {
                var viewsEvents = analytics.Where(e => e.Type == EventType.Viewed).OrderBy(e => e.CreatedAt).ToList();
                var response = new GetAnalyticsResponse
                {
                    Id = context.Message.Id,
                    Views = viewsEvents.Count,
                    LastViewedAt = viewsEvents.FirstOrDefault()?.CreatedAt ?? DateTime.UtcNow,
                };

                await context.RespondAsync(response);
            }
            else
            {
                //TO-DO
            }
        }

    }
}
